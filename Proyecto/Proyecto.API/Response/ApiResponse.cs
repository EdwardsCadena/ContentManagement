namespace Proyecto.API.Response
{
    // Esta línea define una clase pública genérica llamada ApiResponse
    // Esto me permite tener un formato de respuesta consistente en toda la API.
    public class ApiResponse<T>
    {
        // Este es el constructor de la clase ApiResponse
        // Recibe un parámetro de tipo genérico (T) llamado data
        public ApiResponse(T data)
        {
            // Este valor se asigna a la propiedad Data
            Data = data;
        }

        // Esta línea define una propiedad pública de tipo genérico T llamada Data
        // Esta propiedad puede ser obtenida o establecida desde fuera de la clase
        public T Data { get; set; }
    }
}
