using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace RecipeApp.Utility
{
    public class UtilityMethods
    {
        /**
         * Checks if the "newObject" properties has any null value,
         * if so, it won't map the value to the "oldObject".
         */
        public static void UpdateProps<T>(T oldObject, T newObject)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var prop = property.GetValue(newObject, null);
                if (property.CanWrite)
                {
                    bool isInteger = property.PropertyType == typeof(int);
                    if (prop != null && (isInteger ? (int)prop != -1 : true))
                    {
                        property.SetValue(oldObject, property.GetValue(newObject, null), null);
                    }
                }
            }
        }

        /**
         * Upload file to the "wwwroot/images" direction.
         */
        public static string UploadedFile<T>(T file) where T : IFormFile
        {
            string uniqueFileName = uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return "/images/" + uniqueFileName;
        }
    }
}
