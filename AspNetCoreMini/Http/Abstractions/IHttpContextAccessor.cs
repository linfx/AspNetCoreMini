namespace AspNetCoreMini.Http
{
    public interface IHttpContextAccessor
    {
        HttpContext HttpContext { get; set; }
    }
}