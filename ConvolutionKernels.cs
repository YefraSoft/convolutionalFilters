public static class ConvolutionKernels
{
    // ---------------------------------------------------------
    // 1. Filtros Paso Bajo (Low-pass)
    // ---------------------------------------------------------
    public static readonly float[][] PasoBajo =
    [
        [0f,     0.1f,  0f],
        [0.1f,   0.6f,  0.1f],
        [0f,     0.1f,  0f]
    ];

    public static readonly float[][] Smooth =
    [
        [1f/16f, 2f/16f, 1f/16f],
        [2f/16f, 4f/16f, 2f/16f],
        [1f/16f, 2f/16f, 1f/16f]
    ];

    public static readonly float[][] PasoBajo1 =
    [
        [1f/12f, 1f/12f, 1f/12f],
        [1f/12f, 4f/12f, 1f/12f],
        [1f/12f, 1f/12f, 1f/12f]
    ];

    public static readonly float[][] PasoBajo2 =
    [
        [1f/10f, 0f,     1f/10f],
        [1f/10f, 4f/10f, 1f/10f],
        [1f/10f, 0f,     1f/10f]
    ];

    public static readonly float[][] PasoBajo3 =
    [
        [0f,     1f/8f,  0f],
        [1f/8f,  1f/8f,  1f/8f],
        [0f,     1f/8f,  0f]
    ];

    public static readonly float[][] Media3x3 =
    [
        [1f/9f, 1f/9f, 1f/9f],
        [1f/9f, 1f/9f, 1f/9f],
        [1f/9f, 1f/9f, 1f/9f]
    ];

    // ---------------------------------------------------------
    // 2. Filtros Paso Alto (High-pass)
    // ---------------------------------------------------------
    public static readonly float[][] PasoAlto1 =
    [
        [ 1, -2,  1],
        [-2,  4, -2],
        [ 1, -2,  1]
    ];

    public static readonly float[][] PasoAlto2 =
    [
        [-1, -1, -1],
        [-1,  8, -1],
        [-1, -1, -1]
    ];

    public static readonly float[][] PasoAlto3 =
    [
        [ 0, -1,  0],
        [-1,  4, -1],
        [ 0, -1,  0]
    ];

    public static readonly float[][] PasoAltoExtra1 =
    [
        [ 1, -2,  1],
        [-2,  4, -2],
        [ 1, -2,  1]
    ];

    public static readonly float[][] PasoAltoExtra2 =
    [
        [-2,  0, -2],
        [ 0,  8,  0],
        [-2,  0, -2]
    ];

    public static readonly float[][] PasoAltoExtra3 =
    [
        [-1, -2, -1],
        [-2, 12, -2],
        [-1, -2, -1]
    ];

    // ---------------------------------------------------------
    // 3. Sharpen
    // ---------------------------------------------------------
    public static readonly float[][] Sharpen1 =
    [
        [ 1, -2,  1],
        [-2,  5, -2],
        [ 1, -2,  1]
    ];

    public static readonly float[][] Sharpen2 =
    [
        [-1, -1, -1],
        [-1,  9, -1],
        [-1, -1, -1]
    ];

    public static readonly float[][] Sharpen3 =
    [
        [0, -1, 0],
        [-1, 5, -1],
        [0, -1, 0]
    ];

    // ---------------------------------------------------------
    // 4. Detección de bordes (Derivada 1)
    // ---------------------------------------------------------

    // Roberts
    public static readonly float[][] RobertsH =
    [
        [ 0,  0, -1],
        [ 0,  1,  0],
        [ 0,  0,  0]
    ];

    public static readonly float[][] RobertsV =
    [
        [-1, 0, 0],
        [ 0, 0, 0],
        [ 1, 0, 0]
    ];

    // Prewitt
    public static readonly float[][] PrewittH =
    [
        [ 1,  0, -1],
        [ 1,  0, -1],
        [ 1,  0, -1]
    ];

    public static readonly float[][] PrewittV =
    [
        [-1, -1, -1],
        [ 0,  0,  0],
        [ 1,  1,  1]
    ];

    // Sobel
    public static readonly float[][] SobelH =
    [
        [ 1,  0, -1],
        [ 2,  0, -2],
        [ 1,  0, -1]
    ];

    public static readonly float[][] SobelV =
    [
        [-1, -2, -1],
        [ 0,  0,  0],
        [ 1,  2,  1]
    ];

    // Frei-Chen (√2 ≈ 1.41421356)
    private const float SQRT2 = 1.41421356f;

    public static readonly float[][] FreiChenH =
    [
        [ 1,      0,       -1],
        [ SQRT2,  0,       -SQRT2],
        [ 1,      0,       -1]
    ];

    public static readonly float[][] FreiChenV =
    [
        [-1,     -SQRT2,  -1],
        [ 0,       0,      0],
        [ 1,      SQRT2,   1]
    ];

    // ---------------------------------------------------------
    // 5. Laplacianos (Derivada 2)
    // ---------------------------------------------------------
    public static readonly float[][] Laplaciano1 =
    [
        [ 1, -2,  1],
        [-2,  4, -2],
        [ 1, -2,  1]
    ];

    public static readonly float[][] Laplaciano2 =
    [
        [-1, -1, -1],
        [-1,  8, -1],
        [-1, -1, -1]
    ];

    public static readonly float[][] Laplaciano3 =
    [
        [ 0, -1,  0],
        [-1,  4, -1],
        [ 0, -1,  0]
    ];
}
