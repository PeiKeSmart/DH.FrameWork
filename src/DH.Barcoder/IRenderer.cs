namespace Barcoder;

public interface IRenderer {
    void Render(IBarcode barcode, Stream outputStream);
}