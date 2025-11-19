# Editor Piolín

Editor simple WPF para aplicar filtros por convolución a imágenes (C#, .NET 8).

## Descripción
`editor_piolin` es una pequeña aplicación WPF que permite:
- Abrir una imagen (`.jpg`, `.jpeg`, `.png`).
- Seleccionar un kernel de convolución (lista automática desde `ConvolutionKernels.cs`).
- Aplicar el kernel sobre la imagen y visualizar el resultado.

El proyecto está diseñado para aprender y experimentar con filtros espaciales (suavizado, realce, detección de bordes, Laplacianos, etc).

## Estructura relevante
- `MainWindow.xaml` / `MainWindow.xaml.cs` — Interfaz y lógica principal.
- `ConvolutionKernels.cs` — Contiene los kernels definidos como `public static readonly float[][]`.
- Imágenes de ejemplo no se incluyen en el repositorio por defecto.

## Requisitos
- Visual Studio 2022 o superior.
- .NET 8 SDK (proyecto target `.NET 8`).

## Abrir y ejecutar
1. Abre la solución/proyecto en Visual Studio.
2. Compila con __Build > Build Solution__.
3. Ejecuta con __Debug > Start Debugging__ o __Debug > Start Without Debugging__.

## Uso
1. Pulsa `Open Image` y selecciona una imagen.
2. En el combo `Kernel` elige el filtro deseado (nombres descriptivos; el control lee automáticamente los kernels definidos en `ConvolutionKernels.cs`).
3. Pulsa `Aplicar kernel` para procesar la imagen y ver el resultado en `Processed Image`.

## Añadir o editar kernels
- Edita `ConvolutionKernels.cs` y añade o modifica campos `public static readonly float[][]`.
- El `ComboBox` carga por reflexión todos los campos públicos y estáticos de tipo `float[][]`, por lo que no hace falta cambiar la UI para que aparezcan nuevos kernels.

Ejemplo de nueva definición simple:
```csharp
public static readonly float[][] CustomKernel = new float[][]
[
	[ 0, -1, 0 ,
	[ -1, 5, -1 ],
	[0, -1, 0 ]
];


```

## Notas de rendimiento
- El procesamiento usa `Parallel.For` para paralelizar por filas; para imágenes muy grandes puede bloquear la UI. Si necesitas que el UI no se congele, envuelve la llamada en `Task.Run` y actualiza el `Image` con `Dispatcher.Invoke`.
- Para mediciones de rendimiento se puede reutilizar la función `Medir` (si se añade) para ejecutar múltiples iteraciones y calcular tiempos promedio.

## Control de versiones (nota importante)
Este repositorio ha sido configurado para subir únicamente los archivos fuente (`*.cs` y `*.xaml`). Esto significa que no se incluyen archivos de proyecto, binarios ni archivos temporales. Si quieres incluir también la solución y los proyectos (`*.sln`, `*.csproj`), edita el archivo `.gitignore`.

## Contribuir
- Abre issue o PR con cambios.
- Mantén la convención de kernels como `public static readonly float[][]` para que la UI los detecte automáticamente.

## Licencia
- MIT.
