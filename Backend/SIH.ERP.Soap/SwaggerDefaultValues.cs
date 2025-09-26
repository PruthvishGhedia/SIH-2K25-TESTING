using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SIH.ERP.Soap
{
    /// <summary>
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.
    /// </summary>
    /// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <c>Swagger/OpenAPI</c> tooling.
    /// Once they are fixed, this class can be removed.</remarks>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            // Remove the deprecated check as it's not available in this version
            // operation.Deprecated |= apiDescription.IsDeprecated();

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1752#issue-663991077
            foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
                if (operation.Responses.TryGetValue(responseKey, out var response))
                {
                    foreach (var contentType in response.Content.Keys)
                    {
                        if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                        {
                            response.Content.Remove(contentType);
                        }
                    }
                }
            }

            if (operation.Parameters == null)
            {
                return;
            }

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);

                if (description != null)
                {
                    parameter.Description ??= description.ModelMetadata?.Description;

                    if (parameter.Schema.Default == null && description.DefaultValue != null)
                    {
                        // REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
                        var json = System.Text.Json.JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata?.ModelType ?? typeof(object));
                        parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                    }

                    parameter.Required |= description.IsRequired;
                }
            }
        }
    }
}