using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Controllers
{    
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        //Task 1.1.
        [HttpGet]
        [Route("/")]
        public string Get()
        {
            return "Hello, World!";
        }

        //Task 1.2.
        [HttpGet]
        [Route("GetAverage")]
        public float GetAverage([FromQuery(Name = "integers")] int[] integers)
        {

            int sum = 0;
            if (integers.Length==0 || integers==null)
            {
                throw new Exception("The array null or empty");
            }
            else
            {
                for (int i = 0; i < integers.Length; i++)
                {
                    sum += integers[i];
                }
                return (float)sum/integers.Length;
            }

        }

    }
}
