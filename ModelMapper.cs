using System;
using System.Collections.Generic;
using System.Linq;

namespace WeAreGeekers.ModelMapper
{

    /// <summary>
    /// ModelMapper object that contains all mapping setted and method logic for set map
    /// </summary>
    public class ModelMapper
    {

        #region Properties

        /// <summary>
        /// List of all mapping between objects setted
        /// </summary>
        internal List<ModelMapperBuilder> ListModelMapperBuilders { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor of ModelMapper object
        /// </summary>
        public ModelMapper()
        {
            ListModelMapperBuilders = new List<ModelMapperBuilder>();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Set a Mapping between two objects through the action with the ModelMapperBuilder parameter
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="action"></param>
        public void Map<TInput, TOutput>(Action<ModelMapperBuilder<TInput, TOutput>> action)
        {
            // Find Mapping Builder if is just set (take always the last set)
            var findMappingBuilder = ListModelMapperBuilders.Find(f => f.TypeTInput == typeof(TInput) && f.TypeTOutput == typeof(TOutput));
            if (findMappingBuilder != null)
            {
                throw new Exception("It's not possible to create a mapping between types '" + typeof(TInput).Name + "' - '" + typeof(TOutput).Name + "' because it's already exist!");
            }

            // Create new mapping and get it
            ListModelMapperBuilders.Add(new ModelMapperBuilder<TInput, TOutput>());
            findMappingBuilder = ListModelMapperBuilders.Last();

            // Run action
            action((ModelMapperBuilder<TInput, TOutput>)findMappingBuilder);
        }

        #endregion

    }

}
