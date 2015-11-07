using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsGenerator.Models
{
    public enum ArrayFormat
    {
        /// <summary>
        /// Example output: "cars[]=Saab&cars[]=Audi"
        /// </summary>
        DuplicateKeyWithBrackets,
        /// <summary>
        /// Example output: "cars=Saab&cars=Audi"
        /// </summary>
        DuplicateKey,
        /// <summary>
        /// Example output "cars=Saab,Audi"
        /// </summary>
        SingleKey

    }
}
