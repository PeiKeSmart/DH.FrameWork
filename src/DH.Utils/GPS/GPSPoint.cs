namespace DH.GPS;

public class GPSPoint {
    private double lat;// 纬度
    private double lng;// 经度

    public GPSPoint()
    {
    }

    public GPSPoint(double lng, double lat)
    {
        this.lng = lng;
        this.lat = lat;
    }

    public double GetLat()
    {
        return lat;
    }

    public void SetLat(double lat)
    {
        this.lat = lat;
    }

    public double GetLng()
    {
        return lng;
    }

    public void SetLng(double lng)
    {
        this.lng = lng;
    }

}