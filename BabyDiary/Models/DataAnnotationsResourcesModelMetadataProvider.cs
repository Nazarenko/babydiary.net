using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace BabyDiary.Models
{
    public class DataAnnotationsResourcesModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        public DataAnnotationsResourcesModelMetadataProvider(Type resourceType)
        {
            ResourceType = resourceType;
        }

        public Type ResourceType { get; set; }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes,
            Type containerType,
            Func<object> modelAccessor,
            Type modelType,
            string propertyName)
        {
            var attributesList = attributes.ToArray();
            Func<IEnumerable<Attribute>, ModelMetadata> metadataFactory =
                attr => base.CreateMetadata(attr, containerType, modelAccessor, modelType, propertyName);

            SetErrorMessageResources(attributesList.OfType<ValidationAttribute>(), containerType, propertyName);
            SetErrorMessageResources(attributesList.OfType<EmailAddressAttribute>(), containerType, propertyName);

            return metadataFactory(attributesList);
        }

        private void SetErrorMessageResources(IEnumerable<ValidationAttribute> attributes, Type containerType, string propertyName)
        {
            foreach (ValidationAttribute attr in attributes)
            {
                if (!string.IsNullOrEmpty(attr.ErrorMessage)) continue;

                var attributeShortName = attr.GetType().Name.Replace("Attribute", "");
                var containerName = containerType.Name.Replace("Dto", "");
                var resourceKey = containerName + propertyName + attributeShortName;

                var resourceType = attr.ErrorMessageResourceType ?? ResourceType;

                if (PropertyExists(resourceType, resourceKey))
                {
                    attr.ErrorMessageResourceType = resourceType;
                    attr.ErrorMessageResourceName = resourceKey;
                }
                else
                {
                    resourceKey = propertyName + attributeShortName;
                    if (PropertyExists(resourceType, resourceKey))
                    {
                        attr.ErrorMessageResourceType = resourceType;
                        attr.ErrorMessageResourceName = resourceKey;
                    }
                }
            }
        }

        private bool PropertyExists(Type type, string propertyName)
        {
            if (type == null || propertyName == null)
            {
                return false;
            }

            var property = type.GetProperty(propertyName,
                BindingFlags.NonPublic
                | BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.Instance);

            if (property == null)
            {
                return false;
            }

            var getter = property.GetGetMethod(true);

            return getter.IsPublic || getter.IsAssembly || getter.IsFamilyOrAssembly;

        }
    }

}