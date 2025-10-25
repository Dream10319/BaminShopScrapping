using Nancy.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaeminShopScrapping
{
    public partial class Form1 : Form
    {
        Thread th = null;
        bool isStarted = false;
        int offset = 0;
        int catindex = 0;
        int subtotal = 0;
        int shopcountersave = 0;
        int locationnumsave = 0;
        long counter = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(th != null)
                th.Abort();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                offset = (int)OffsetUpdown.Value;
                catindex = (int)CategoryUpdown.Value;
                if(!isStarted)
                {
                    button1.Text = "Pause";
                    isStarted = true;
                    timer1.Start();
                }
                else
                {
                    button1.Text = "Start";
                    isStarted = false;
                    if (th != null)
                        th.Suspend();
                    return;
                }
                th = new Thread(new ThreadStart(() =>
                {
                    
                    string filePath = AppDomain.CurrentDomain.BaseDirectory + @"locationinfo.txt"; // Adjust the path to where your file is stored.

                    // Regular expression to match one or more spaces or tabs
                    Regex delimiterRegex = new Regex(@"[\s\t]+");

                    // List to hold the coordinate tuples
                    List<(string Latitude, string Longitude)> coordinates = new List<(string, string)>();

                    // Read the file line by line
                    if(radioButton1.Checked)
                    {
                        try
                        {
                            this.Invoke(new Action(() =>
                            {
                                Lat.Enabled = false;
                                Lon.Enabled = false;
                            }));
                            using (StreamReader reader = new StreamReader(filePath))
                            {
                                string line;
                                bool isFirstLine = true; // Variable to skip the header

                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (isFirstLine)
                                    {
                                        isFirstLine = false; // Skip the first line which is the header
                                        continue;
                                    }

                                    // Split the line into latitude and longitude parts using the regex
                                    string[] parts = delimiterRegex.Split(line.Trim());
                                    if (parts.Length >= 2)
                                    {
                                        // Add the latitude and longitude as a tuple to the list
                                        coordinates.Add((parts[0], parts[1]));
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Can't find locationinfo.txt file, please check that");
                        }
                    }
                    else if (radioButton2.Checked)
                    {
                        coordinates.Add((Lat.Text, Lon.Text));
                    }

                    string strUrl = string.Format(@"https://shopdp-api.baemin.com/display-groups/FOOD_CATEGORY?latitude=37.5450159&longitude=127.1368066&sessionId=b4e3292329dfd570f054c8&carrier=302780&site=7jWXRELC2e&dvcid=OPUD6086af457479a7bb&adid=aede849f-5e9c-499f-827f-cb4e5c65d801&deviceModel=SM-G9500&appver=15.13.3&oscd=2&osver=32&dongCode=11140102&zipCode=04522&actionTrackingKey=4557");
                    RestClient client = new RestClient(strUrl);
                    RestRequest request = new RestRequest();
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Connection", "Keep-Alive");
                    request.AddHeader("Host", "shopdp-api.baemin.com");
                    request.AddHeader("User-Agent", "and1_15.13.3");
                    request.AddHeader("USER-BAEDAL", "xdDyIUGCY4ZCPw9mg2buMflWiTG4SFCx6mvudZZi0YHHYEdbXn/n/WW1+p3IdAS4R1y7detb05BqAbaoq8kr+wYr/cahiuVIFYHlnKlaspsjKaAWuCyijvi0eXSK/U7410UTzkIlfbwyVR5ZkQmvKnf2kOU4xfA8PamV5QBV6f4xRdWNZqghEgz75WCmCq7K");
                    string strReturn = client.ExecuteGet(request).Content;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    dynamic data = jss.Deserialize<dynamic>(strReturn);
                    dynamic categories = data["data"]["displayCategories"];

                    // Output the list of coordinates
                    int locationNum = locationnumsave;
                    int shopcounter = shopcountersave;
                    for (int n = 0; n < coordinates.Count; n++)
                    {
                        var (Latitude, Longitude) = coordinates[n];
                        locationNum++;
                        this.Invoke(new Action(() =>
                        {
                            Lat.Text = Latitude.ToString();
                            Lon.Text = Longitude.ToString();
                            LocationNum.Text = "Location" + locationNum.ToString();
                        }));
                        for (int l = catindex; l < categories.Count; l++)
                        {
                            if (categories[l]["CODE"] == "FOOD_CATEGORY_ALL") continue;
                            this.Invoke(new Action(() =>
                            {
                                Category.Text = categories[l]["text"].ToString();
                            }));
                            int shopcount = 2000;
                            int totalcount = subtotal;
                            for (int i = offset; i <= (int)(shopcount / 30); i++)
                            {
                                try
                                {
                                    strUrl = string.Format(@"https://shopdp-api.baemin.com/v4/FOOD_CATEGORY/shops?displayCategory={3}&longitude={0}&latitude={1}&sort=SORT__DEFAULT&filter=&offset={2}&limit=30&extension=&perseusSessionId=1718023403008.788454282780365941.FWy8AA9FNv&memberNumber=000000000000&sessionId=b4e3292329dfd570f054c8&carrier=302780&site=7jWXRELC2e&dvcid=OPUD6086af457479a7bb&adid=aede849f-5e9c-499f-827f-cb4e5c65d801&deviceModel=SM-G9500&appver=15.13.3&oscd=2&osver=32&dongCode=11140102&zipCode=04522&actionTrackingKey=4557", Longitude.ToString(), Latitude.ToString(), 25 * i, categories[l]["text"].ToString());
                                    File.WriteAllText("log.txt", Environment.NewLine + "lat:" + Lat.Text + ", lon:" + Lon.Text + ", offset:" + i.ToString() + ", catindex:" + l.ToString());
                                    this.Invoke(new Action(() =>
                                    {
                                        CategoryUpdown.Value = l;
                                        OffsetUpdown.Value = i;
                                    }));
                                    client = new RestClient(strUrl);
                                    strReturn = client.ExecuteGet(request).Content;
                                    if (strReturn.Contains("SUCCESS"))
                                    {
                                        jss = new JavaScriptSerializer();
                                        data = jss.Deserialize<dynamic>(strReturn);
                                        dynamic shops = data["data"]["shops"];
                                        shopcount = (int)data["data"]["totalCount"];
                                        foreach (var shop in shops)
                                        {
                                            totalcount++;
                                            string shopnumber = shop["shopInfo"]["shopNumber"].ToString();
                                            if (!string.IsNullOrEmpty(shopnumber))
                                            {
                                                try
                                                {
                                                    strUrl = string.Format(@"https://shop-detail-api.baemin.com/api/v1/shops/{0}/detail?mem=&lat={1}&lng={2}&lat4Distance=37.4567894&lng4Distance=126.8822667&campaignId=11393767&displayGroup=FOOD_CATEGORY&sort=SORT__DEFAULT&filter=&emphasizeMenuGroup%5BmenuClick%5D%5BmenuIds%5D=1026484251&exposedDeliveryType=MP&bypassData=eyJmcmFuY2hpc2VOb0ZvckNoZWNrRGVsaXZlcnlEaXN0YW5jZUxpbWl0IjpudWxsLCJzaG9wTm9Gb3JDaGVja0RlbGl2ZXJ5RGlzdGFuY2VMaW1pdCI6bnVsbH0%3D&sessionId=525107d52d1b6e6836cc4c45767a&carrier=302780&site=7jWXRELC2e&dvcid=OPUDf48850e556873dfc&adid=4bd027e0-d307-4740-8866-a9e00e4861f1&deviceModel=SM-G9500&appver=15.13.3&oscd=2&osver=32&dongCode=41210103&zipCode=14309&perseusClientId=1758202708009.785757936779787763.8E8Pqm5o1O&perseusSessionId=1761365114540.532459589018434225.aBJy3Ck1MN&actionTrackingKey=4557", shopnumber, Latitude.ToString(), Longitude.ToString());
                                                    client = new RestClient(strUrl);
                                                    RestRequest detailrequest = new RestRequest();
                                                    detailrequest.AddHeader("Accept-Encoding", "gzip, deflate");
                                                    detailrequest.AddHeader("Authorization", "bearer guest");
                                                    detailrequest.AddHeader("Connection", "Keep-Alive");
                                                    detailrequest.AddHeader("Host", "shop-detail-api.baemin.com");
                                                    detailrequest.AddHeader("User-Agent", "and1_15.13.3");
                                                    detailrequest.AddHeader("USER-BAEDAL", "xdDyIUGCY4ZCPw9mg2buMflWiTG4SFCx6mvudZZi0YHHYEdbXn/n/WW1+p3IdAS4R1y7detb05BqAbaoq8kr+wYr/cahiuVIFYHlnKlaspsjKaAWuCyijvi0eXSK/U7410UTzkIlfbwyVR5ZkQmvKnf2kOU4xfA8PamV5QBV6f4xRdWNZqghEgz75WCmCq7K");

                                                    strReturn = client.ExecuteGet(detailrequest).Content;
                                                    if (strReturn.Contains("SUCCESS"))
                                                    {
                                                        var dir = $"ShopMenus\\{shopnumber}";
                                                        Directory.CreateDirectory(dir);

                                                        string shopdetailUrl = string.Format($@"https://shop-detail-api.baemin.com/api/v1/shops/{shopnumber}/info-detail?mem=200805019111&lat={Lat.Text}&lng={Lon.Text}&exposedDeliveryType=SINGLE&sessionId=a4250394e7d61e7a5e250ea00c&carrier=302780&site=7jWXRELC2e&dvcid=OPUDf48850e556873dfc&adid=4bd027e0-d307-4740-8866-a9e00e4861f1&deviceModel=SM-G9500&appver=15.13.3&oscd=2&osver=32&dongCode=41210103&zipCode=14309&perseusClientId=1758202708009.785757936779787763.8E8Pqm5o1O&perseusSessionId=1758274579543.753060450530256865.BiX4vxCGU3&actionTrackingKey=Organic");
                                                        RestClient detailclient = new RestClient(shopdetailUrl);
                                                        var inforequest = new RestRequest();
                                                        inforequest.AddHeader("Accept-Encoding", "gzip, deflate");
                                                        inforequest.AddHeader("Connection", "Keep-Alive");
                                                        inforequest.AddHeader("Host", "shop-detail-api.baemin.com");
                                                        inforequest.AddHeader("User-Agent", "and1_15.13.3");
                                                        inforequest.AddHeader("USER-BAEDAL", "xdDyIUGCY4ZCPw9mg2buMflWiTG4SFCx6mvudZZi0YHHYEdbXn/n/WW1+p3IdAS4R1y7detb05BqAbaoq8kr+wYr/cahiuVIFYHlnKlaspsjKaAWuCyijvi0eXSK/U7410UTzkIlfbwyVR5ZkQmvKnf2kOU4xfA8PamV5QBV6f4xRdWNZqghEgz75WCmCq7K");
                                                        string shopdetail = detailclient.ExecuteGet(detailrequest).Content;

                                                        File.WriteAllText(string.Format(@"{0}\{1}-detail.json", dir, shopnumber), shopdetail);

                                                        File.WriteAllText(string.Format(@"{0}\{1}.json", dir, shopnumber), strReturn);
                                                        if (shop["shopInfo"] != null)
                                                        File.WriteAllText(string.Format(@"{0}\{1}-logo.json", dir, shopnumber), shop["shopInfo"].ToString());
                                                        shopcounter++;
                                                        jss = new JavaScriptSerializer();
                                                        data = jss.Deserialize<dynamic>(strReturn);
                                                        //dynamic groupMenus = data["data"]["shop_menu"]["menu_ord"]["groupMenus"];
                                                        dynamic groupMenus = data["data"]["menuPan"]["menuGroups"];
                                                        foreach (var groupMenu in groupMenus)
                                                        {
                                                            if(groupMenu["menus"] != null)
                                                            foreach (var menu in groupMenu["menus"])
                                                            {
                                                                strUrl = string.Format($@"https://shop-detail-api.baemin.com/api/v1/shops/{shopnumber}/menus/{menu["menuId"]}?mem=000000000000&availableMenuReceivingTypes=DELIVERY&availableOrderTypes=DELIVERY&selectedOrderType=DELIVERY&sessionId=052656a48346e0a1e738a54&carrier=302780&site=7jWXRELC2e&dvcid=OPUDf48850e556873dfc&adid=4bd027e0-d307-4740-8866-a9e00e4861f1&deviceModel=SM-G9500&appver=15.13.3&oscd=2&osver=32&dongCode=41210103&zipCode=14309&perseusClientId=1758202708009.785757936779787763.8E8Pqm5o1O&perseusSessionId=1759550313005.599510435655348598.LnowXl1Imo&actionTrackingKey=Organic");
                                                                client = new RestClient(strUrl);
                                                                strReturn = client.ExecuteGet(inforequest).Content;
                                                                File.WriteAllText(string.Format(@"{0}\{1}-{2}.json", dir, shopnumber, menu["menuId"]), strReturn);
                                                                this.Invoke(new Action(() =>
                                                                {
                                                                    progressBar1.Value = (int)((10000 * totalcount) / shopcount);
                                                                }));
                                                                this.Invoke(new Action(() =>
                                                                {
                                                                    ShopCounter.Text = shopcounter.ToString();
                                                                }));
                                                                shopcountersave = shopcounter;
                                                                subtotal = totalcount;
                                                            }  
                                                        }  
                                                    }
                                                }
                                                catch (Exception ex)
                                                {

                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        shopcount = 0;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    
                                }

                            }

                        }
                        catindex = 0;
                        offset = 0;
                        locationnumsave = locationNum;

                        // Remove the item that was just used
                        coordinates.RemoveAt(n);
                        n--; // Adjust index since we removed an element
                        SaveCoordinates(filePath, coordinates);
                    }
                    if(locationNum > 0)
                    {
                        progressBar1.Value = 10000;
                        MessageBox.Show("Successfully done!!!");
                    }
                }));
                th.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(LocationNum.Text);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Lat.Enabled = true;
            Lon.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(isStarted)
            {
                counter++;
            }
            if(counter == 600)
            {
                RestartApplication();
            }
        }

        private void RestartApplication()
        {
            string appPath = Application.ExecutablePath;
            Process.Start(appPath);
            Application.Exit(); 
        }

        static void SaveCoordinates(string filePath, List<(string Latitude, string Longitude)> coordinates)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("latitude   longitude"); // Write header
                foreach (var (lat, lon) in coordinates)
                {
                    writer.WriteLine($"{lat}\t{lon}"); // Save remaining coordinates
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string logFilePath = "log.txt";

            if (File.Exists(logFilePath))
            {
                string[] lines = File.ReadAllLines(logFilePath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("lat:"))
                    {
                        string[] parts = line.Split(',');
                        foreach (string part in parts)
                        {
                            string[] keyValue = part.Split(':');
                            if (keyValue.Length == 2)
                            {
                                string key = keyValue[0].Trim();
                                string value = keyValue[1].Trim();

                                //if (key == "lat") txtLatitude.Text = value;
                                //if (key == "lon") txtLongitude.Text = value;
                                if (key == "offset") OffsetUpdown.Value = Convert.ToDecimal(value);
                                if (key == "catindex") CategoryUpdown.Value = Convert.ToDecimal(value) + 1;
                            }
                        }
                    }
                }
            }
            button1_Click(this, EventArgs.Empty);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (th != null)
                th.Abort();
            base.OnFormClosed(e);
        }
    }
}
