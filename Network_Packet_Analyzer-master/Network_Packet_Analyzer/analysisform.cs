using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Network_Packet_Analyzer
{
    public partial class analysisform : Form
    {
        private List<ListViewItem> connectionItems;
        public analysisform(List<ListViewItem> connections)
        {
            InitializeComponent();
            connectionItems = connections;
            LoadConnections();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ARPchart_Click(object sender, EventArgs e)
        {

        }

        private void analysisform_Load(object sender, EventArgs e)
        {

        }

        private void LoadConnections()
        {
            // Clear previous chart data and setup chart areas
            ARPchart.Series.Clear();
            ARPchart.ChartAreas.Clear();
            ARPchart.ChartAreas.Add(new ChartArea("ARPChartArea"));
            Series arpSeries = new Series("ARP Connections")
            {
                ChartType = SeriesChartType.Bar,  // Bar chart for ARP
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Int32,
                BorderWidth = 2,
                BorderColor = Color.FromArgb(50, 50, 50),  // Dark gray border for subtle contrast
                IsValueShownAsLabel = true,  // Show values on top of the bars
                LabelForeColor = Color.White,  // Label text color (white for contrast)
                Font = new Font("Arial", 10, FontStyle.Bold),  // Bold font for labels
                BackGradientStyle = GradientStyle.LeftRight,  // Gradient for bars
                BackSecondaryColor = Color.FromArgb(255, 102, 102),  // Light red as secondary color
                Color = Color.FromArgb(255, 51, 51),  // Primary bar color (vibrant red)
                ShadowColor = Color.FromArgb(100, 0, 0, 0),  // Soft shadow for depth
                ShadowOffset = 4,  // Shadow offset for a professional look
                IsVisibleInLegend = false,  // Hide from the legend for a cleaner look
                CustomProperties = "BarWidth=0.8"  // Adjust bar width to make them less crowded
            };


            TCPchart.Series.Clear();
            TCPchart.ChartAreas.Clear();
            TCPchart.ChartAreas.Add(new ChartArea("TCPChartArea"));
            Series tcpSeries = new Series("TCP Connections")
            {
                ChartType = SeriesChartType.Spline,  // Spline chart for smooth lines
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Int32,
                BorderWidth = 3,
                BorderColor = Color.FromArgb(0, 102, 204),  // Distinct blue line color
                IsValueShownAsLabel = false,  // Disable value labels on the line
                Font = new Font("Arial", 10, FontStyle.Bold),  // Bold font for any text labels
                MarkerStyle = MarkerStyle.Circle,  // Add circular markers at data points
                MarkerSize = 8,  // Size of the data point markers
                MarkerColor = Color.FromArgb(0, 102, 204),  // Color of the markers to match the line
                BorderDashStyle = ChartDashStyle.Solid,  // Solid line for the curve
                ShadowColor = Color.FromArgb(100, 0, 0, 0),  // Shadow color for depth
                ShadowOffset = 3,  // Shadow offset for a floating effect
                BackSecondaryColor = Color.Transparent,  // No background color
                BackGradientStyle = GradientStyle.None  // No gradient fill for the line
            };


            UDPchart.Series.Clear();
            UDPchart.ChartAreas.Clear();
            UDPchart.ChartAreas.Add(new ChartArea("UDPChartArea"));
            Series udpSeries = new Series("UDP Connections")
            {
                ChartType = SeriesChartType.Column,  // Column chart for UDP
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Int32,
                BorderWidth = 2,
                Color = Color.FromArgb(0, 153, 0),  // Greenish color for UDP
                IsValueShownAsLabel = true,  // Show values on top of the bars
                LabelForeColor = Color.White,  // White color for label text for contrast
                ShadowOffset = 3  // Optional: Adds a shadow effect to give depth
            };


            IPNETchart.Series.Clear();
            IPNETchart.ChartAreas.Clear();
            IPNETchart.ChartAreas.Add(new ChartArea("IPNETChartArea"));
            Series ipnetSeries = new Series("IPNET Connections")
            {
                ChartType = SeriesChartType.Doughnut,  // Doughnut chart for IPNET
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Int32,
                BorderWidth = 3,
                BorderColor = Color.White,
                BorderDashStyle = ChartDashStyle.Solid,  // Solid border for a clean look
                IsValueShownAsLabel = true,  // Show values inside the doughnut slices
                LabelForeColor = Color.White,  // Label text color for contrast
                LabelFormat = "#,#",  // Format labels with commas for better readability
                ShadowColor = Color.FromArgb(50, 0, 0, 0),  // Soft shadow for depth
                ShadowOffset = 5,  // Shadow effect for depth
                BackGradientStyle = GradientStyle.TopBottom,  // Gradient background for doughnut segments
                BackSecondaryColor = Color.FromArgb(100, 100, 255),  // Light blue secondary color
                Color = Color.FromArgb(0, 153, 204),  // Main color for the doughnut chart (light blue)
            };


            // Create a dictionary to store the count of each unique remote address and protocol combination
            Dictionary<string, int> remoteAddressProtocolCount = new Dictionary<string, int>();

            // Iterate over the connectionItems and classify them into ARP, TCP, UDP, or IPNET
            foreach (var item in connectionItems)
            {
                // Access the protocol column (index 5)
                string protocol = item.SubItems[5].Text; // Protocol is in the 6th column (index 5)

                // Assuming remote address is in the third column (index 2)
                string remoteAddress = item.SubItems[2].Text;
                Console.WriteLine($"Protocol: {protocol}, Remote Address: {remoteAddress}");

                // Create a composite key using remote address and protocol
                string key = remoteAddress + "_" + protocol;

                // Increment the count of the remote address and protocol combination in the dictionary
                if (remoteAddressProtocolCount.ContainsKey(key))
                {
                    remoteAddressProtocolCount[key]++;
                }
                else
                {
                    remoteAddressProtocolCount[key] = 1;
                }
            }

            // Now, add the aggregated data to the charts
            foreach (var kvp in remoteAddressProtocolCount)
            {
                // Get the remote address, protocol, and its count
                string[] keyParts = kvp.Key.Split('_');
                string remoteAddress = keyParts[0];
                string protocol = keyParts[1];
                int count = kvp.Value;

                // Add the data to the respective chart based on protocol
                if (protocol == "ARP")
                {
                    arpSeries.Points.AddXY(remoteAddress, count);
                }
                else if (protocol == "TCP")
                {
                    tcpSeries.Points.AddXY(remoteAddress, count);
                }
                else if (protocol == "UDP")
                {
                    udpSeries.Points.AddXY(remoteAddress, count);
                }
                else if (protocol == "IPNET")
                {
                    ipnetSeries.Points.AddXY(remoteAddress, count);
                }
            }

            // Add the series to the charts
            ARPchart.Series.Add(arpSeries);
            TCPchart.Series.Add(tcpSeries);
            UDPchart.Series.Add(udpSeries);
            IPNETchart.Series.Add(ipnetSeries);

            // Set the chart background colors to more subtle tones
            ARPchart.BackColor = Color.FromArgb(245, 245, 245);  // Light gray background
            TCPchart.BackColor = Color.FromArgb(245, 245, 245);  // Light gray background
            UDPchart.BackColor = Color.FromArgb(245, 245, 245);  // Light gray background
            IPNETchart.BackColor = Color.FromArgb(245, 245, 245);  // Light gray background

            // Set chart title fonts and colors to make them more aesthetic
            Title arpTitle = new Title("ARP Connections Distribution", Docking.Top, new Font("Segoe UI", 16, FontStyle.Bold), Color.FromArgb(255, 51, 51)); // Red shade for ARP
            arpTitle.ShadowColor = Color.FromArgb(100, 0, 0, 0); // Soft shadow color
            arpTitle.ShadowOffset = 2; // Slight shadow offset for depth
            ARPchart.Titles.Add(arpTitle);

            Title tcpTitle = new Title("TCP Connections Distribution", Docking.Top, new Font("Segoe UI", 16, FontStyle.Bold), Color.FromArgb(0, 102, 204));
            tcpTitle.ShadowColor = Color.FromArgb(100, 0, 0, 0); // Soft shadow color
            tcpTitle.ShadowOffset = 2;
            TCPchart.Titles.Add(tcpTitle);

            Title udpTitle = new Title("UDP Connections Distribution", Docking.Top, new Font("Segoe UI", 16, FontStyle.Bold), Color.FromArgb(0, 153, 0));
            udpTitle.ShadowColor = Color.FromArgb(100, 0, 0, 0); // Soft shadow color
            udpTitle.ShadowOffset = 2;
            UDPchart.Titles.Add(udpTitle);

            Title ipnetTitle = new Title("IPNET Connections Distribution", Docking.Top, new Font("Segoe UI", 16, FontStyle.Bold), Color.FromArgb(255, 204, 102)); // Purplish yellow shade
            ipnetTitle.ShadowColor = Color.FromArgb(100, 0, 0, 0); // Soft shadow color
            ipnetTitle.ShadowOffset = 2;
            IPNETchart.Titles.Add(ipnetTitle);

            // Customize chart legend to match the modern aesthetic
            ARPchart.Legends[0].BackColor = Color.FromArgb(245, 245, 245);
            TCPchart.Legends[0].BackColor = Color.FromArgb(245, 245, 245);
            UDPchart.Legends[0].BackColor = Color.FromArgb(245, 245, 245);
            IPNETchart.Legends[0].BackColor = Color.FromArgb(245, 245, 245);

            // Optional: add shadow or 3D effect to charts for a modern look (this can be adjusted for aesthetics)
            ARPchart.Palette = ChartColorPalette.SeaGreen;
            TCPchart.Palette = ChartColorPalette.BrightPastel;
            UDPchart.Palette = ChartColorPalette.Pastel;
            IPNETchart.Palette = ChartColorPalette.EarthTones;
        }





        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
