using System;

namespace Microsoft.AspNetCore.Mvc
{
    public class ProducesResponseAttribute : ProducesResponseTypeAttribute
    {
        public ProducesResponseAttribute(int statusCode, Type type = null)  
        : base(type ?? typeof(void), statusCode)  
        { 
          
        }  
    } 
}