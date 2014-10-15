using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinerClub.Converters
{
    /// <summary>
    /// Generic interface for all converters.
    /// </summary>
    /// <typeparam name="TIn">Input parameter type.</typeparam>
    /// <typeparam name="TOut">Output parameter type.</typeparam>
    public interface IConverter<in TIn, out TOut>
    {
        /// <summary>
        /// Conterts input type object to output type object.
        /// </summary>
        /// <param name="input">Instance of input type.</param>
        /// <returns>Instance of output type.</returns>
        TOut Convert(TIn input);
    }
}
