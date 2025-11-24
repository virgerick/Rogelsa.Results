# Rogelsa.Results

[![NuGet](https://img.shields.io/nuget/v/Rogelsa.Results.svg)](https://www.nuget.org/packages/Rogelsa.Results/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Rogelsa.Results es una librería ligera para .NET que facilita la implementación del patrón Result, ayudando a manejar errores, estados y respuestas de manera clara, consistente y sin excepciones en exceso.

## Características Principales

- Implementación genérica del patrón Result para operaciones que pueden fallar
- Tipos de errores predefinidos para casos comunes (NotFound, BadRequest, Conflict, etc.)
- Métodos de extensión para un manejo funcional de resultados
- Soporte para operaciones asíncronas
- Tipos de éxito predefinidos para respuestas comunes (Ok, Created, NoContent, etc.)
- Paginación integrada para colecciones

## Instalación

Puedes instalar el paquete NuGet usando el siguiente comando en la consola del Administrador de Paquetes NuGet:

```bash
Install-Package Rogelsa.Results
```

O a través de la CLI de .NET:

```bash
dotnet add package Rogelsa.Results
```

## Uso Básico

### Crear resultados exitosos

```csharp
// Para operaciones que devuelven un valor
Result<Usuario> resultado = Result<Usuario>.Success(new Usuario { Id = 1, Nombre = "Ejemplo" });

// Para operaciones que no devuelven un valor
Result resultadoOperacion = Result.Success();

// Usando tipos de éxito predefinidos
Result<Usuario> usuarioCreado = Result<Usuario>.Success(new Created<Usuario>(usuario));
Result<Usuario> usuarioActualizado = Result<Usuario>.Success(new Ok<Usuario>(usuario));
Result sinContenido = Result.Success(new NoContent());
```

### Manejar errores

```csharp
// Crear un resultado fallido
Result<Usuario> resultadoFallido = Result<Usuario>.Failure(new NotFound("Usuario no encontrado"));

// Tipos de error disponibles:
// - BadRequest: Solicitud incorrecta (400)
// - Unauthorized: No autorizado (401)
// - Forbidden: Prohibido (403)
// - NotFound: Recurso no encontrado (404)
// - Conflict: Conflicto (409)
// - ValidationFailure: Errores de validación (400)
// - Error: Error genérico (500)

// Ejemplo con errores de validación
var errores = new ValidationsErrorsBuilder()
    .AddError("Email", "El correo electrónico es requerido")
    .AddError("Password", "La contraseña debe tener al menos 8 caracteres")
    .Build();
    
Result validacionFallida = Result.Failure(new ValidationFailure(errores));
```

### Manejar resultados

```csharp
public Result<Usuario> ObtenerUsuario(int id)
{
    try
    {
        var usuario = _repositorio.ObtenerPorId(id);
        if (usuario == null)
            return Result<Usuario>.Failure(new NotFound("Usuario no encontrado"));
            
        return Result<Usuario>.Success(usuario);
    }
    catch (Exception ex)
    {
        return Result<Usuario>.Failure(new Error(ex.Message));
    }
}

// Uso
var resultado = ObtenerUsuario(1);

// Verificar si fue exitoso
if (resultado.IsSuccess)
{
    // Obtener el valor
    Usuario usuario = resultado.Value;
    Console.WriteLine(usuario.Nombre);
}
else
{
    // Manejar el error
    Console.WriteLine($"Error: {resultado.Error.Message}");
}
```

### Usando métodos de extensión

```csharp
// Mapear un resultado a otro tipo
Result<string> resultadoNombre = resultado.Map(usuario => usuario.Nombre);

// Manejar tanto éxito como fracaso en un solo paso
string mensaje = resultado.Match(
    onSuccess: usuario => $"Usuario encontrado: {usuario.Nombre}",
    onFailure: error => $"Error: {error.Message}"
);

// Encadenar operaciones
var resultadoComplejo = ObtenerUsuario(1)
    .Map(usuario => usuario.Nombre.ToUpper())
    .Bind(nombreMayusculas => ProcesarNombre(nombreMayusculas));
```

### Paginación

```csharp
public Result<Paged<Usuario>> ObtenerUsuariosPaginados(int pagina, int tamanoPagina)
{
    try
    {
        var usuarios = _repositorio.ObtenerTodos()
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToList();
            
        int total = _repositorio.Contar();
        
        var paged = new Paged<Usuario>(usuarios, pagina, tamanoPagina, total);
        
        return Result<Paged<Usuario>>.Success(paged);
    }
    catch (Exception ex)
    {
        return Result<Paged<Usuario>>.Failure(new Error(ex.Message));
    }
}

// Uso
var resultado = ObtenerUsuariosPaginados(1, 10);
if (resultado.IsSuccess)
{
    Paged<Usuario> paginaUsuarios = resultado.Value;
    Console.WriteLine($"Mostrando {paginaUsuarios.Items.Count} de {paginaUsuarios.Total} usuarios");
}
```

## Extensiones y Métodos de Extensión

### Extensiones para Éxitos (ResultSuccessExtensions)

Estas extensiones proporcionan métodos de conveniencia para crear resultados exitosos de manera más concisa.

```csharp
// Crear un resultado exitoso sin valor de retorno
Result resultado = Result.Ok();

// Crear un resultado exitoso con un valor
Result<Usuario> usuario = Result.Ok(new Usuario { Id = 1, Nombre = "Ejemplo" });

// Crear un resultado exitoso sin contenido (204 No Content)
Result sinContenido = Result.NoContent();

// Crear un resultado exitoso con estado Created (201)
Result<Usuario> creado = Result.Created(new Usuario { Id = 2, Nombre = "Nuevo Usuario" });

// Crear un resultado exitoso con estado Deleted
Result<Usuario> eliminado = Result.Deleted(new Usuario { Id = 3, Nombre = "Eliminado" });
```

### Extensiones para Fallos (ResultFailureExtensions)

Estas extensiones proporcionan métodos para crear resultados fallidos de manera más legible.

```csharp
// Errores comunes
Result noEncontrado = Result.NotFound("El recurso no existe");
Result noAutorizado = Result.Unauthorized("No autorizado");
Result prohibido = Result.Forbidden("No tiene permisos");
Result conflicto = Result.Conflict("El correo ya está en uso");
Result errorValidacion = Result.BadRequest("Datos inválidos");
Result errorInterno = Result.Error("Error interno del servidor");

// Con tipos genéricos
Result<Usuario> usuarioNoEncontrado = Result<Usuario>.NotFound("Usuario no encontrado");
```

### Extensiones Funcionales (FunctionalExtensions)

Estas extensiones permiten trabajar con resultados de manera funcional.

```csharp
// Transformar un resultado exitoso
Result<string> nombreUsuario = usuario.Map(u => u.Nombre);

// Encadenar operaciones que devuelven Result
Result<string> resultado = ObtenerUsuario(1)
    .Map(usuario => usuario.Nombre.ToUpper())
    .Bind(nombreMayusculas => ProcesarNombre(nombreMayusculas));

// Manejar tanto éxito como fracaso
string mensaje = resultado.Match(
    onSuccess: nombre => $"Operación exitosa: {nombre}",
    onFailure: error => $"Error: {error.Message}"
);

// Para operaciones asíncronas
public async Task<Result<Usuario>> ObtenerUsuarioAsync(int id)
{
    return await _repositorio.ObtenerPorIdAsync(id)
        .MapAsync(usuario => usuario ?? throw new InvalidOperationException("No encontrado"))
        .MapErrorAsync(_ => Result<Usuario>.NotFound("Usuario no encontrado"));
}
```

## Builders

### ValidationsErrorsBuilder

El `ValidationsErrorsBuilder` facilita la construcción de errores de validación estructurados.

```csharp
// Crear un error de validación
var errores = new ValidationsErrorsBuilder()
    .AddError("Email", "El correo es requerido")
    .AddError("Email", "El formato del correo no es válido")
    .AddError("Password", "La contraseña debe tener al menos 8 caracteres")
    .AddError("Password", "La contraseña debe contener al menos un número")
    .Build();

// Usar el error de validación en un Result
Result resultadoInvalido = Result.Failure(new ValidationFailure(errores));

// En un controlador de API
if (!ModelState.IsValid)
{
    var errores = new ValidationsErrorsBuilder();
    foreach (var error in ModelState)
    {
        foreach (var mensaje in error.Value.Errors)
        {
            errores.AddError(error.Key, mensaje.ErrorMessage);
        }
    }
    return BadRequest(errores.Build());
}
```

## Manejo Avanzado con Pattern Matching

### Usando `Match` con Pattern Matching

El método `Match` es ideal cuando necesitas manejar tanto el éxito como el error en una sola expresión y devolver un valor.

```csharp
// Ejemplo con tipos de éxito específicos
string mensaje = resultado.Match(
    onSuccess: success => success switch
    {
        Ok<Usuario> ok => $"Operación exitosa: {ok.Value.Nombre}",
        Created<Usuario> created => $"Usuario creado con ID: {created.Value.Id}",
        NoContent _ => "Operación exitosa sin contenido",
        _ => "Operación exitosa"
    },
    onFailure: error => error switch
    {
        NotFound nf => $"Error 404: {nf.Message}",
        ValidationFailure vf => $"Error de validación: {string.Join(", ", vf.Errors.SelectMany(e => e.Value))}",
        BadRequest br => $"Solicitud incorrecta: {br.Message}",
        _ => $"Error inesperado: {error.Message}"
    }
);
```

### Usando `Switch` para Efectos Secundarios

El método `Switch` es útil cuando necesitas realizar acciones basadas en el resultado sin devolver un valor.

```csharp
// Ejemplo con acciones basadas en el tipo de resultado
resultado.Switch(
    onSuccess: success =>
    {
        switch (success)
        {
            case Ok<Usuario> ok:
                _logger.LogInformation($"Usuario obtenido: {ok.Value.Nombre}");
                break;
                
            case Created<Usuario> created:
                _logger.LogInformation($"Nuevo usuario creado con ID: {created.Value.Id}");
                _notificaciones.EnviarBienvenida(created.Value.Email);
                break;
                
            case NoContent _:
                _logger.LogInformation("Operación completada sin contenido");
                break;
        }
    },
    onFailure: error =>
    {
        switch (error)
        {
            case NotFound _:
                _logger.LogWarning($"Recurso no encontrado: {error.Message}");
                break;
                
            case ValidationFailure vf:
                _logger.LogWarning("Errores de validación: {Errores}", 
                    string.Join(", ", vf.Errors.SelectMany(e => $"{e.Key}: {string.Join(", ", e.Value)}")));
                break;
                
            case Conflict _:
                _logger.LogWarning("Conflicto detectado: {Mensaje}", error.Message);
                break;
                
            default:
                _logger.LogError(error, "Error inesperado");
                break;
        }
    }
);
```

### En Controladores de API

```csharp
[HttpPost]
public IActionResult CrearUsuario([FromBody] CrearUsuarioDto dto)
{
    return _servicio.CrearUsuario(dto).Match(
        onSuccess: success => success switch
        {
            Created<Usuario> created => 
                CreatedAtAction(nameof(ObtenerUsuario), new { id = created.Value.Id }, created.Value),
                
            _ => Ok() // Para otros tipos de éxito
        },
        onFailure: error => error switch
        {
            ValidationFailure vf => 
                BadRequest(new { Errors = vf.Errors }),
                
            Conflict c => 
                Conflict(c.Message),
                
            _ => StatusCode(500, new { Error = error.Message })
        }
    );
}
```

### En Servicios con Operaciones Complejas

```csharp
public Result<Usuario> ProcesarPago(int usuarioId, decimal monto)
{
    return ObtenerUsuario(usuarioId)
        .Bind(usuario =>
        {
            if (usuario.Saldo < monto)
                return Result<Usuario>.Failure(new BadRequest("Saldo insuficiente"));
                
            usuario.Saldo -= monto;
            return GuardarCambios(usuario);
        })
        .Match(
            onSuccess: success => success switch
            {
                Ok<Usuario> ok => 
                    Result<Usuario>.Success(new Ok<Usuario>(ok.Value)),
                    
                NoContent _ => 
                    Result<Usuario>.Failure(new Error("No se pudo completar la operación")),
                    
                _ => (Result<Usuario>)success
            },
            onFailure: error => error switch
            {
                NotFound _ => 
                    Result<Usuario>.Failure(new BadRequest("Usuario no encontrado")),
                    
                _ => Result<Usuario>.Failure(error)
            }
        );
}
```

### Consideraciones sobre el Uso de Pattern Matching

1. **Orden de los patrones**: Los patrones se evalúan en orden. Coloca los patrones más específicos primero.

2. **Caso por defecto**: Siempre incluye un caso por defecto (`_`) para manejar tipos inesperados.

3. **Rendimiento**: El pattern matching es muy eficiente en C# y se optimiza bien en tiempo de compilación.

4. **Legibilidad**: Usa patrones descriptivos para mejorar la legibilidad del código.

5. **Tipos anidados**: Puedes anidar patrones para manejar estructuras de datos complejas.

## Patrones de Uso Recomendados

1. **En controladores de API**:
   ```csharp
   [HttpGet("{id}")]
   public IActionResult ObtenerUsuario(int id)
   {
       var resultado = _servicio.ObtenerUsuario(id);
       
       return resultado.Match(
           onSuccess: usuario => Ok(usuario),
           onFailure: error => error switch
           {
               NotFound nf => NotFound(nf.Message),
               ValidationFailure vf => BadRequest(vf.Errors),
               _ => StatusCode(500, error.Message)
           }
       );
   }
   ```

2. **En servicios**:
   ```csharp
   public Result<Usuario> ActualizarUsuario(int id, ActualizarUsuarioDto dto)
   {
       var usuario = _repositorio.ObtenerPorId(id);
       if (usuario == null)
           return Result<Usuario>.Failure(new NotFound("Usuario no encontrado"));
           
       // Validaciones de negocio
       if (dto.Email != usuario.Email && _repositorio.ExisteEmail(dto.Email))
           return Result<Usuario>.Failure(new Conflict("El correo electrónico ya está en uso"));
           
       // Actualizar usuario
       usuario.Nombre = dto.Nombre;
       usuario.Email = dto.Email;
       
       _repositorio.Actualizar(usuario);
       _unidadDeTrabajo.GuardarCambios();
       
       return Result<Usuario>.Success(new Ok<Usuario>(usuario));
   }
   ```

## Contribución

¡Las contribuciones son bienvenidas! Siéntete libre de enviar un Pull Request o reportar problemas.

## Licencia

Este proyecto está licenciado bajo la licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.
