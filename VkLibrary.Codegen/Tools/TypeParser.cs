﻿using System.Linq;
using Newtonsoft.Json.Linq;
using VkLibrary.Codegen.Types.TitleCase;

namespace VkLibrary.Codegen.Tools
{
    public static class TypeParser
    {
        public static string AddNullabilityIfNeed(string type)
        {
            //TODO: parse is_require
            if (type == "int" || type == "Boolean" || type == "double")
                type += "?";

            return type;
        }

        public static ICustomCaseTitle ParseType(JToken body)
        {
            if (body.Type == JTokenType.Array)
            {
                Log.Instance.Message($"Can't parse type from {body}. Probably problem with \"items\": []");
                //Know problem: bug with 
                //"items": {
                //    "type": "array",
                //    "items": []
                //},
                return UndefinedCaseTitle.Of("object");
            }

            if (body["type"] == null) return MatchDefaultType(GetTypeFromRef(body));

            if (body["type"].Type == JTokenType.Array)
            {
                Log.Instance.Message($"Type composition: {body["type"].ToLogString()}");
                return UndefinedCaseTitle.Of("object");
            }

            if (body["type"].Value<string>() == "array") return CamelCaseArrayTitle.Of(ParseType(body["items"]));

            return MatchDefaultType(body.Value<string>("type"));
        }

        public static string GetTypeFromRef(JToken body)
        {
            return body["$ref"]
                .ToString()
                .Split('/')
                .Last();
        }

        public static ICustomCaseTitle MatchDefaultType(string type)
        {
            string result = type switch
            {
                "base" => "bool",
                "integer" => "int",
                "base_bool_int" => "int",
                "number" => "double",
                "String" => "string",
                _ => type
            };

            if (result != type)
                return UndefinedCaseTitle.Of(result);

            return CamelCaseTitle.Of(result);
        }
    }
}