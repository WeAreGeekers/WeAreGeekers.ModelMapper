using System;
using WeAreGeekers.ModelMapper.Settings;

namespace WeAreGeekers.ModelMapper.Extensions
{

    /// <summary>
    /// Static class with extension methods of objects
    /// </summary>
    public static class ObjectExtensions
    {

        /// <summary>
        /// Map the current object to a T object with the settings of mapping setted in the WeAreGeekers.ModelMapper
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TOutput MapTo<TInput, TOutput>(this TInput obj)
        {
            // Find Mapper Builder
            var findMapperBuilder = ModelMapperSettings
                .ModelMapper
                .ListModelMapperBuilders
                .Find(f => f.TypeTInput == obj.GetType() && f.TypeTOutput == typeof(TOutput));

            // Generate exception if builder was not finded
            if (findMapperBuilder == null) throw new Exception("You can't map automatically the object type '" + obj.GetType().Name + "' to '" + typeof(TOutput).Name + "' because not exist a mapping set between the two types");

            // Return object based on mapping function
            return findMapperBuilder.GetMappingFuncInternal<TInput, TOutput>(findMapperBuilder.ListModelMapperBuilderProperties).Invoke(obj);
        }

    }

}
