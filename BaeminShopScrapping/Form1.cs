using Nancy.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    string filePath = @"locationinfo.txt"; // Adjust the path to where your file is stored.

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

                    string strUrl = string.Format(@"https://shopdp-api.baemin.com/display-groups/BAEMIN?latitude=37.5450159&longitude=127.1368066&sessionId=b4e3292329dfd570f054c8&carrier=302780&site=7jWXRELC2e&dvcid=OPUD6086af457479a7bb&adid=aede849f-5e9c-499f-827f-cb4e5c65d801&deviceModel=SM-G9500&appver=12.23.0&oscd=2&osver=32&dongCode=11140102&zipCode=04522&ActionTrackingKey=Organic");
                    RestClient client = new RestClient(strUrl);
                    RestRequest request = new RestRequest();
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Connection", "Keep-Alive");
                    request.AddHeader("Host", "shopdp-api.baemin.com");
                    request.AddHeader("User-Agent", "and1_12.23.0");
                    request.AddHeader("USER-BAEDAL", "W/OnG34HSvOVmxn4McyeRzEK3Ldc9+ruPokFIKgQcm2WNLVj7olTxJ0p1zFrDCSZNSn3awPLFRB7F7SLiZOBxY5dp1ciNVpOX4dPZMCvouEui+ogBfrZb1Pjq9MjIKgPTZ0PsoEXEDVbIayRYi3FqT19ktIosfMynUFcdCj6Qdpz/m8+MSoP9SsUAtxB8t2W+KD/mAlr8ojTCP1x0n5tWG31re4ZOComuGzviiuX7wpkfSLsshuaV9mNVXcByHNw");
                    string strReturn = client.ExecuteGet(request).Content;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    dynamic data = jss.Deserialize<dynamic>(strReturn);
                    dynamic categories = data["data"]["displayCategories"];

                    // Output the list of coordinates
                    int locationNum = locationnumsave;
                    int shopcounter = shopcountersave;
                    foreach (var (Latitude, Longitude) in coordinates)
                    {
                        locationNum++;
                        int nCatTmp, nOffTmp;
                        if (catindex != 0)
                        {
                            nCatTmp = catindex;
                        }
                        this.Invoke(new Action(() =>
                        {
                            Lat.Text = Latitude.ToString();
                            Lon.Text = Longitude.ToString();
                            LocationNum.Text = "Location" + locationNum.ToString();
                        }));
                        for (int l = catindex; l < categories.Count; l++)
                        {
                            this.Invoke(new Action(() =>
                            {
                                Category.Text = categories[l]["text"].ToString();
                            }));
                            int shopcount = 2000;
                            int totalcount = subtotal;
                            for (int i = offset; i <= (int)(shopcount / 25); i++)
                            {
                                try
                                {
                                    strUrl = string.Format(@"https://shopdp-api.baemin.com/v3/BAEMIN/shops?displayCategory={3}&longitude={0}&latitude={1}&sort=SORT__DEFAULT&filter=&offset={2}&limit=25&extension=&perseusSessionId=1738983528177.747641265099996874.MPdNU86586&memberNumber=000000000000&sessionId=17b5632f7742526e26a535687&carrier=302780&site=7jWXRELC2e&dvcid=OPUDf48850e556873dfc&adid=4bd027e0-d307-4740-8866-a9e00e4861f1&deviceModel=SM-G9500&appver=12.23.0&oscd=2&osver=32&dongCode=28237101&zipCode=21404&ActionTrackingKey=Organic", Longitude.ToString(), Latitude.ToString(), 25 * i, categories[l]["code"].ToString());
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
                                                    strUrl = string.Format(@"https://shopdp-api.baemin.com/v8/shop/{0}/detail?lat={1}&lng={2}&limit=25&mem=&memid=&defaultreview=N&campaignId=2353465&displayGroup=BAEMIN&lat4Distance=37.5670653&lng4Distance=126.98168738&filter=&sessionId=17b5632f7742526e26a535687&carrier=302780&site=7jWXRELC2e&dvcid=OPUDf48850e556873dfc&adid=4bd027e0-d307-4740-8866-a9e00e4861f1&deviceModel=SM-G9500&appver=12.23.0&oscd=2&osver=32&dongCode=&zipCode=&ActionTrackingKey=Organic", shopnumber, Latitude.ToString(), Longitude.ToString());
                                                    client = new RestClient(strUrl);
                                                    strReturn = client.ExecuteGet(request).Content;
                                                    if (strReturn.Contains("SUCCESS"))
                                                    {
                                                        var dir = $"ShopMenus\\{shopnumber}";
                                                        Directory.CreateDirectory(dir);
                                                        File.WriteAllText(string.Format(@"{0}\{1}.json", dir, shopnumber), strReturn);
                                                        if (shop["shopInfo"] != null)
                                                        File.WriteAllText(string.Format(@"{0}\{1}-logo.json", dir, shopnumber), shop["shopInfo"].ToString());
                                                        shopcounter++;
                                                        jss = new JavaScriptSerializer();
                                                        data = jss.Deserialize<dynamic>(strReturn);
                                                        dynamic groupMenus = data["data"]["shop_menu"]["menu_ord"]["groupMenus"];
                                                        foreach(var groupMenu in groupMenus)
                                                        {
                                                            if(groupMenu["menus"] != null)
                                                            foreach (var menu in groupMenu["menus"])
                                                            {
                                                                strUrl = string.Format($@"https://shopdp-api.baemin.com/v1/shops/{shopnumber}/menus/{menu["menuId"]}?useDelivery=true&useTakeout=false&useTableOrder=false&sessionId=17b5632f7742526e26a535687&carrier=302780&site=7jWXRELC2e&dvcid=OPUDf48850e556873dfc&adid=4bd027e0-d307-4740-8866-a9e00e4861f1&deviceModel=SM-G9500&appver=12.23.0&oscd=2&osver=32&dongCode=&zipCode=&ActionTrackingKey=Organic");
                                                                client = new RestClient(strUrl);
                                                                strReturn = client.ExecuteGet(request).Content;
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
            if(counter == 1200)
            {
                timer1.Stop();
                counter = 0;
                isStarted = false;
                if (th != null)
                {
                    th.Suspend();
                    MessageBox.Show("Stopped");
                }
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                counter++;
            }
            if (counter == 300)
            {
                timer2.Stop();
                counter = 0;
                isStarted = true;
                if (th != null)
                {
                    th.Resume();
                    MessageBox.Show("Resumed");
                }
                timer1.Start();
            }
        }
    }
}
