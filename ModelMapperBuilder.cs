using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using WeAreGeekers.ModelMapper.Extensions;

namespace WeAreGeekers.ModelMapper
{

    /// <summary>
    /// ModelMapperBuilder abstract class
    /// </summary>
    public abstract class ModelMapperBuilder
    {

        #region Properties

        /// <summary>
        /// Type of the input object
        /// </summary>
        internal Type TypeTInput { get; set; }

        /// <summary>
        /// Type of the output object
        /// </summary>
        internal Type TypeTOutput { get; set; }

        /// <summary>
        /// List of properties that's set the mapping
        /// </summary>
        internal List<ModelMapperBuilderProperty> ListModelMapperBuilderProperties { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor of ModelMapperBuilder
        /// </summary>
        /// <param name="typeTInput"></param>
        /// <param name="typeTOutput"></param>
        internal ModelMapperBuilder(Type typeTInput, Type typeTOutput)
        {
            TypeTInput = typeTInput;
            TypeTOutput = typeTOutput;
            ListModelMapperBuilderProperties = new List<ModelMapperBuilderProperty>();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Return the LambdaExpression of the mapping
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="listModelMapperBuilderProperties"></param>
        /// <returns></returns>
        protected Expression<Func<TInput, TOutput>> GetMappingLambdaExpression<TInput, TOutput>(List<ModelMapperBuilderProperty> listModelMapperBuilderProperties)
        {
            // Check exceptions
            if (TypeTInput != typeof(TInput)) throw new Exception("Type of TInput '" + typeof(TInput).Name + "' is not the same of TypeTInput '" + TypeTInput.Name);
            if (TypeTOutput != typeof(TOutput)) throw new Exception("Type of TOutput '" + typeof(TOutput).Name + "' is not the same of TypeTOutput '" + TypeTOutput.Name);

            // Create Parameter [p => ]
            ParameterExpression parameter = Expression.Parameter(typeof(TInput), "p");

            // Create new statement [ new Data() ]
            NewExpression newStatement = Expression.New(typeof(TOutput));

            // Create bindings
            List<MemberBinding> listBindings = new List<MemberBinding>();

            listModelMapperBuilderProperties.ForEach(fe =>
            {
                // Get property of the 'from' entry
                MemberExpression propertyEntryFrom = Expression.Property(parameter, fe.PropertyInfoFrom);

                // Set value of 'to' entry "Field1 = o.Field1"
                listBindings.Add(Expression.Bind(fe.PropertyInfoTo, propertyEntryFrom));
            });

            // Create initialization [ new Data { Field1 = o.Field1, Field2 = o.Field2 } ]
            MemberInitExpression memberInitExpression = Expression.MemberInit(newStatement, listBindings);

            // Create lambda expression [ o => new Data { Field1 = o.Field1, Field2 = o.Field2 } ]
            Expression<Func<TInput, TOutput>> lambdaExpression = Expression.Lambda<Func<TInput, TOutput>>(memberInitExpression, parameter);

            // return lambda expression
            return lambdaExpression;
        }

        /// <summary>
        /// Return the LambdaExpression of the mapping
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="listModelMapperBuilderProperties"></param>
        /// <returns></returns>
        internal Expression<Func<TInput, TOutput>> GetMappingLambdaExpressionInternal<TInput, TOutput>(List<ModelMapperBuilderProperty> listModelMapperBuilderProperties)
        {
            return GetMappingLambdaExpression<TInput, TOutput>(listModelMapperBuilderProperties);
        }

        /// <summary>
        /// Return the Function of the mapping
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="listModelMapperBuilderProperties"></param>
        /// <returns></returns>
        protected Func<TInput, TOutput> GetMappingFunc<TInput, TOutput>(List<ModelMapperBuilderProperty> listModelMapperBuilderProperties)
        {
            return GetMappingLambdaExpression<TInput, TOutput>(listModelMapperBuilderProperties).Compile();
        }

        /// <summary>
        /// Return the Function of the mapping
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="listModelMapperBuilderProperties"></param>
        /// <returns></returns>
        internal Func<TInput, TOutput> GetMappingFuncInternal<TInput, TOutput>(List<ModelMapperBuilderProperty> listModelMapperBuilderProperties)
        {
            return GetMappingFunc<TInput, TOutput>(listModelMapperBuilderProperties).Compile();
        }

        #endregion

    }



    /// <summary>
    /// ModelMapperBuilder based with input and output types based on TInput and TOutput generic objects
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public class ModelMapperBuilder<TInput, TOutput> : ModelMapperBuilder
    {

        #region Constructor 

        /// <summary>
        /// Constructor of ModelMapperBuilder
        /// </summary>
        public ModelMapperBuilder() : base(typeof(TInput), typeof(TOutput)) { }

        #endregion


        #region Methods

        /// <summary>
        /// Method that set the association of mapping between two property
        /// </summary>
        /// <param name="expressionsFieldTInput"></param>
        /// <param name="expressionsFieldTOutput"></param>
        /// <returns></returns>
        public ModelMapperBuilder<TInput, TOutput> MapProperty(Expression<Func<TInput, object>> expressionsFieldTInput, Expression<Func<TOutput, object>> expressionsFieldTOutput)
        {
            // Extract propertyInfo
            PropertyInfo propertyInfoTInput = expressionsFieldTInput.GetPropertyInfo();
            PropertyInfo propertyInfoTOutput = expressionsFieldTOutput.GetPropertyInfo();

            // Check propertyInfo
            if (propertyInfoTInput == null) throw new ArgumentNullException("expressionsFieldTInput");
            if (propertyInfoTOutput == null) throw new ArgumentNullException("expressionsFieldTOutput");

            // Check if it's just set (if it's set remove it and set this that's it the last)
            var findPropertyMap = ListModelMapperBuilderProperties.Find(f => f.PropertyInfoFrom == propertyInfoTInput);
            if (findPropertyMap != null) ListModelMapperBuilderProperties.Remove(findPropertyMap);

            // Add ModelMapperBuilderProperty
            ListModelMapperBuilderProperties.Add(new ModelMapperBuilderProperty(propertyInfoTInput, propertyInfoTOutput));

            // Return object
            return this;
        }

        #endregion

    }

}
