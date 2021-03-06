﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using VkLibrary.Codegen.Models;
using VkLibrary.Codegen.Tools;
using VkLibrary.Codegen.Types;

namespace VkLibrary.Codegen.Generators
{
    public static class FileGenerator
    {
        public static void Process(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);

            var provider = new JsonSchemaProvider();
            GenerateFromObject(provider, $"{directoryPath}/Objects/");
            GenerateFromResponses(provider, $"{directoryPath}/Responses/");
            GenerateFromMethods(provider, $"{directoryPath}/");

            provider.GetUndefined().ForEach(i => Log.Instance.Message(i.Body.ToString()));
        }

        private static void GenerateFromObject(JsonSchemaProvider provider, string directoryPath)
        {
            List<ClassDescriptor> classes = provider.GetObjectClassDescriptor();
            List<EnumDescriptor> enums = provider.GetObjectEnumDescriptor();

            foreach (ClassDescriptor classDescriptor in classes)
            {
                classDescriptor.MergePropertiesFromBaseClasses(classes);
                CompilationUnitSyntax unit = ClassGenerator.Generate(CommonGenerator.ObjectNamespace, classDescriptor, EntityType.ObjectClass);
                WriteToFile($"{directoryPath}{classDescriptor.Scope}/{classDescriptor.Title.ToSharpString()}.cs", unit);
            }

            foreach (EnumDescriptor enumDescriptor in enums)
            {
                CompilationUnitSyntax unit = EnumGenerator.Generate(CommonGenerator.ObjectNamespace, enumDescriptor, EntityType.ObjectEnum);
                WriteToFile($"{directoryPath}{enumDescriptor.Scope}/{enumDescriptor.Title.ToSharpString()}.cs", unit);
            }
        }

        private static void GenerateFromResponses(JsonSchemaProvider provider, string directoryPath)
        {
            List<ClassDescriptor> responses = provider.GetResponseClassDescriptors();
            foreach (ClassDescriptor classDescriptor in responses)
            {
                CompilationUnitSyntax unit = ClassGenerator.Generate(CommonGenerator.ResponseNamespace, classDescriptor, EntityType.Response);
                WriteToFile($"{directoryPath}{classDescriptor.Scope}/{classDescriptor.Title.ToSharpString()}.cs", unit);
            }
        }

        private static void GenerateFromMethods(JsonSchemaProvider provider, string directoryPath)
        {
            List<IGrouping<string, MethodDescriptor>> grouped = 
                provider
                    .GetMethodDescriptors()
                    .GroupBy(m => m.Scope.ToSharpString())
                    .ToList();

            foreach (IGrouping<string, MethodDescriptor> methodDescriptors in grouped)
            {
                var title = $"{methodDescriptors.Key}";
                CompilationUnitSyntax unit = MethodGenerator.Generate(CommonGenerator.MethodNamespace, title, methodDescriptors.ToList(), EntityType.Method);
                WriteToFile($"{directoryPath}Methods/{title}.cs", unit);
            }
        }

        private static void WriteToFile(string path, CompilationUnitSyntax content)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using var workspace = new AdhocWorkspace();
            using var stream = new StreamWriter(File.Open(path, FileMode.Create));

            SyntaxNode formated = Formatter.Format(content, workspace);
            stream.Write(formated.ToString());
        }
    }
}