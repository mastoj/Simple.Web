﻿using Newtonsoft.Json.Linq;

namespace Simple.Web.JsonNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Links;
    using Newtonsoft.Json;
    using System.Linq;
    using Newtonsoft.Json.Serialization;

    class LinkConverter<T> : JsonConverter
    {
        private readonly Action<JsonWriter, T, JsonSerializer> _writeProperties;
        private readonly Func<object, IEnumerable<object>> _linkEnumerator;

        private LinkConverter(Func<object, IEnumerable<object>> linkEnumerator)
        {
//            _writeProperties = writeProperties;
            _linkEnumerator = linkEnumerator;
        }

        public static LinkConverter<T> New(Func<object, IEnumerable<object>> linkEnumerator, IContractResolver contractResolver)
        {
            //var writer = Expression.Parameter(typeof(JsonWriter));
            //var value = Expression.Parameter(typeof(T));
            //var serializer = Expression.Parameter(typeof(JsonSerializer));

            //var setters = CreateWriteValueExpressions(writer, value, serializer, contractResolver);

            //var block = Expression.Block(setters);
            //var lambda = Expression.Lambda<Action<JsonWriter, T, JsonSerializer>>(block, writer, value, serializer);

            return new LinkConverter<T>(linkEnumerator);
        }

        //private static IEnumerable<Expression> CreateWriteValueExpressions(ParameterExpression writer, ParameterExpression value, ParameterExpression serializer, IContractResolver contractResolver)
        //{
        //    var contract = contractResolver.ResolveContract(typeof (T)) as JsonObjectContract;
        //    if (contract != null)
        //    {
        //        foreach (var property in contract.Properties)
        //        {
        //            var getValueMethod = typeof (IValueProvider).GetMethod("GetValue");
        //            var valueProvider = Expression.Constant(property.ValueProvider);
        //            yield return
        //                Expression.Call(writer, LinkConverter.WritePropertyName,
        //                                Expression.Constant(property.PropertyName));
        //            var getValue = Expression.Call(valueProvider, getValueMethod, value);
        //            yield return Expression.Call(serializer, LinkConverter.Serialize, writer, getValue);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var info in typeof (T).GetProperties())
        //        {
        //            yield return
        //                Expression.Call(writer, LinkConverter.WritePropertyName,
        //                                Expression.Constant(ToCamelCase(info.Name)));
        //            var getValue = Expression.Convert(Expression.Property(value, info), typeof (object));
        //            yield return Expression.Call(serializer, LinkConverter.Serialize, writer, getValue);
        //        }
        //    }
        //}

        //private static string ToCamelCase(string str)
        //{
        //    if (string.IsNullOrWhiteSpace(str)) return str;
        //    if (str.Length == 1) return str.ToLowerInvariant();
        //    return char.ToLowerInvariant(str[0]) + str.Substring(1);
        //}

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var js = new JsonSerializer()
            {
                ContractResolver = serializer.ContractResolver
            };
            JToken t = JToken.FromObject(value, js);
            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);                
            }
            else
            {
                JObject o = (JObject)t;

//                writer.WritePropertyName("links");
                var links = _linkEnumerator(value);
                foreach (var link in links.OfType<Link>())
                {
                    if (string.IsNullOrWhiteSpace(link.Type))
                    {
                        link.Type = "application/json";
                    }
                    else if (!link.Type.EndsWith("+json"))
                    {
                        link.Type = link.Type + "+json";
                    }
                }
                o.Add("links", JToken.FromObject(links, js));
                o.WriteTo(writer);
                //var linksToken = JToken.FromObject(links);
                //linksToken.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
    }
}