using System.Globalization;

namespace MisticFy.API.src.Middleware
{
    public class CultureMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            List<CultureInfo> supportedCultures = [.. CultureInfo.GetCultures(CultureTypes.AllCultures)];

            string requestedCulture = context.Request.Headers.AcceptLanguage.ToString();

            CultureInfo culture = new("en-US");

            if(!string.IsNullOrWhiteSpace(requestedCulture) && supportedCultures.Exists(language => language.Name.Equals(requestedCulture, StringComparison.InvariantCultureIgnoreCase)))
            {
                culture = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }
    }
}
