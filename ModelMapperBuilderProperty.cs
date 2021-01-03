using System.Reflection;

namespace WeAreGeekers.ModelMapper
{

    /// <summary>
    /// Class that contains the information about the builder of the mapper between two properties
    /// </summary>
    public class ModelMapperBuilderProperty
    {

        #region Properties

        /// <summary>
        /// PropertyInfo of 'from' object
        /// </summary>
        internal PropertyInfo PropertyInfoFrom { get; set; }

        /// <summary>
        /// PropertyInfo of 'to' object
        /// </summary>
        internal PropertyInfo PropertyInfoTo { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor of ModelMapperBuilderProperty
        /// </summary>
        /// <param name="propertyInfoFrom"></param>
        /// <param name="propertyInfoTo"></param>
        internal ModelMapperBuilderProperty(PropertyInfo propertyInfoFrom, PropertyInfo propertyInfoTo)
        {
            PropertyInfoFrom = propertyInfoFrom;
            PropertyInfoTo = propertyInfoTo;
        }

        #endregion

    }

}
