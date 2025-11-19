using Microsoft.Win32;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace editor_piolin
{
    public partial class MainWindow : Window
    {

        private BitmapImage? _image;

        public MainWindow()
        {
            InitializeComponent();
            PopulateKernelCombo();
        }

        private void PopulateKernelCombo()
        {
            var descriptive = new Dictionary<string, string>()
            {
                ["PasoBajo"] = "Paso bajo (centro fuerte)",
                ["Smooth"] = "Suavizado (aprox. Gaussiano)",
                ["PasoBajo1"] = "Paso bajo 1 (difuso)",
                ["PasoBajo2"] = "Paso bajo 2 (asimétrico)",
                ["PasoBajo3"] = "Paso bajo 3 (cruce)",
                ["Media3x3"] = "Media 3x3 (promedio)",
                ["PasoAlto1"] = "Paso alto 1 (realce)",
                ["PasoAlto2"] = "Paso alto 2 (bordes fuertes)",
                ["PasoAlto3"] = "Paso alto 3 (enfoque)",
                ["PasoAltoExtra1"] = "Paso alto extra 1",
                ["PasoAltoExtra2"] = "Paso alto extra 2",
                ["PasoAltoExtra3"] = "Paso alto extra 3",
                ["Sharpen1"] = "Sharpen 1 (enfoque)",
                ["Sharpen2"] = "Sharpen 2 (enfoque fuerte)",
                ["Sharpen3"] = "Sharpen clásico (3x3)",
                ["RobertsH"] = "Roberts Horizontal (bordes)",
                ["RobertsV"] = "Roberts Vertical (bordes)",
                ["PrewittH"] = "Prewitt Horizontal",
                ["PrewittV"] = "Prewitt Vertical",
                ["SobelH"] = "Sobel Horizontal",
                ["SobelV"] = "Sobel Vertical",
                ["FreiChenH"] = "Frei-Chen Horizontal",
                ["FreiChenV"] = "Frei-Chen Vertical",
                ["Laplaciano1"] = "Laplaciano 1",
                ["Laplaciano2"] = "Laplaciano 2",
                ["Laplaciano3"] = "Laplaciano 3"
            };

            var kernelType = typeof(ConvolutionKernels);
            var fields = kernelType.GetFields(BindingFlags.Public | BindingFlags.Static);

            // Contenedor simple para mostrar nombre y retener kernel
            foreach (var f in fields.OrderBy(f => f.Name))
            {
                if (f.FieldType != typeof(float[][]))
                    continue;

                var kernel = (float[][])f.GetValue(null)!;
                string display = descriptive.TryGetValue(f.Name, out var desc) ? $"{desc} — {f.Name}" : $"{SplitCamelCase(f.Name)} — {f.Name}";
                KernelCombo.Items.Add(new KernelItem(display, kernel));
            }

            if (KernelCombo.Items.Count > 0)
                KernelCombo.SelectedIndex = 0;
        }

        private sealed record KernelItem(string DisplayName, float[][] Kernel)
        {
            public override string ToString() => DisplayName;
        }

        private static string SplitCamelCase(string s)
        {
            return System.Text.RegularExpressions.Regex.Replace(s, "(\\B[A-Z])", " $1");
        }

        private void GetImage(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            OpenFileDialog dlg = new()
            {
                Filter = "images|*.jpg;*.jpeg;*.png"
            };
            _image = new BitmapImage();
            if (dlg.ShowDialog() == true)
            {
                _image.BeginInit();
                _image.UriSource = new Uri(dlg.FileName);
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.EndInit();
                origphoto.Source = _image;
            }
            Mouse.OverrideCursor = null;
        }

        private void ApplyKernel_Click(object sender, RoutedEventArgs e)
        {
            if (_image == null)
            {
                MessageBox.Show("Carga primero una imagen.", "Sin imagen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (KernelCombo.SelectedItem is not KernelItem item)
            {
                MessageBox.Show("Selecciona un kernel.", "Sin selección", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                // Llamada directa a la función de convolución ya existente
                prophoto.Source = ConvolveImage(item.Kernel, in _image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error aplicando kernel: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private static WriteableBitmap ConvolveImage(float[][] kernel, in BitmapImage src)
        {
            if (kernel.Length > kernel[0].Length)
                throw new ArgumentException("Kernel height must be less than or equal to kernel width.");

            int width = src.PixelWidth;
            int height = src.PixelHeight;
            int stride = width * 4;
            byte[] pixels = new byte[height * stride];
            byte[] output = new byte[height * stride];

            src.CopyPixels(pixels, stride, 0);

            int kernelSize = kernel.Length;
            int kernelRadius = kernelSize / 2;

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    float sumR = 0, sumG = 0, sumB = 0;

                    // Aplicar el kernel
                    for (int ky = 0; ky < kernelSize; ky++)
                    {
                        for (int kx = 0; kx < kernelSize; kx++)
                        {
                            // Calcular las coordenadas
                            int pixelY = y + ky - kernelRadius;
                            int pixelX = x + kx - kernelRadius;

                            pixelY = (pixelY + height) % height;
                            pixelX = (pixelX + width) % width;

                            // Obtener el índice del píxel
                            int pixelIndex = pixelY * stride + pixelX * 4;

                            // Aplicar el valor del kernel
                            float kernelValue = kernel[ky][kx];
                            sumB += pixels[pixelIndex + 0] * kernelValue;
                            sumG += pixels[pixelIndex + 1] * kernelValue;
                            sumR += pixels[pixelIndex + 2] * kernelValue;
                        }
                    }
                    // Calcular el índice de salida
                    int outputIndex = y * stride + x * 4;
                    output[outputIndex + 0] = (byte)Math.Clamp(sumB, 0, 255);
                    output[outputIndex + 1] = (byte)Math.Clamp(sumG, 0, 255);
                    output[outputIndex + 2] = (byte)Math.Clamp(sumR, 0, 255);
                    output[outputIndex + 3] = 255;
                }
            });

            WriteableBitmap result = new(width, height, 96, 96, PixelFormats.Bgra32, null);
            result.WritePixels(new Int32Rect(0, 0, width, height), output, stride, 0);

            return result;
        }
    }
}