﻿using Network_Packet_Analyzer.Connections;
using Network_Packet_Analyzer.Connections.Enums;
using Network_Packet_Analyzer.Connections.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Packet_Analyzer
{
    public partial class Form1 : Form
    {
        private ConnectionsMonitor connectionsMonitor;
        private bool isLoadedConnections = false;
        private List<ListViewItem> originalItems = new List<ListViewItem>();

        public Form1()
        {
            InitializeComponent();
            AddColumnsToListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeConnectionsMonitor();
        }

        private void AddColumnsToListView()
        {
            var columns = new[]
            {
                new ColumnHeader { Text = "Local Address", Width = 100 },
                new ColumnHeader { Text = "Local Port", Width = 60 },
                new ColumnHeader { Text = "Remote Address", Width = 100 },
                new ColumnHeader { Text = "Remote Port", Width = 75 },
                new ColumnHeader { Text = "MAC Address", Width = 100 },
                new ColumnHeader { Text = "Protocol", Width = 60 },
                new ColumnHeader { Text = "State", Width = 70 },
                new ColumnHeader { Text = "Process ID", Width = 70 },
                new ColumnHeader { Text = "Process Name", Width = 100 }
            };

            listViewConnections.Columns.AddRange(columns);
            listViewConnections.Scrollable = true;
            listViewConnections.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }

        private void InitializeConnectionsMonitor()
        {
            connectionsMonitor = new ConnectionsMonitor(true);
            connectionsMonitor.NewPacketsConnectionLoad += UpdateListView;
            connectionsMonitor.NewPacketConnectionStarted += OnPacketConnectionStarted;
            connectionsMonitor.NewPacketConnectionEnded += OnPacketConnectionEnded;
            connectionsMonitor.StartListening();
        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections && listViewConnections.InvokeRequired)
            {
                var liIt = CreateListViewItem(packet);
                if (IsPacketFilterMonitor(packet) && !listViewConnections.Items.Contains(liIt))
                {
                    listViewConnections.Invoke((Action)(() =>
                    {
                        listViewConnections.Items.Add(liIt);
                    }));
                }
            }
            UpdateStatusLabel();
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections && listViewConnections.InvokeRequired)
            {
                var liIt = CreateListViewItem(packet);
                if (listViewConnections.Items.Contains(liIt))
                {
                    listViewConnections.Invoke((Action)(() =>
                    {
                        listViewConnections.Items.Remove(liIt);
                    }));
                }
            }
            UpdateStatusLabel();
        }

        //private void UpdateListView(object sender, PacketConnectionInfo[] packets)
        //{
        //    UpdateStatusLabel();
        //    if (isLoadedConnections == false)
        //    {
        //        var listViewItems = ConvertFromPackArrayToListView(packets);
        //        listViewConnections.Invoke((Action)(() =>
        //        {
        //            listViewConnections.Items.Clear();
        //            listViewConnections.Items.AddRange(listViewItems);
        //            originalItems = listViewItems.ToList(); // Cập nhật danh sách item gốc
        //        }));
        //        isLoadedConnections = true;
        //    }
        //    UpdateStatusLabel();
        //}

        private void UpdateListView(object sender, PacketConnectionInfo[] packets)
        {
            UpdateStatusLabel();
            if (isLoadedConnections == false)
            {
                // Apply the filter before converting to ListViewItems
                var filteredPackets = packets.Where(packet => IsPacketFilterMonitor(packet)).ToArray();

                // Convert the filtered PacketConnectionInfo[] to ListViewItem[]
                var listViewItems = ConvertFromPackArrayToListView(filteredPackets);

                listViewConnections.Invoke((Action)(() =>
                {
                    listViewConnections.Items.Clear();
                    listViewConnections.Items.AddRange(listViewItems);
                    originalItems = listViewItems.ToList(); // Update the originalItems list with filtered items
                }));

                isLoadedConnections = true;
            }
            UpdateStatusLabel();
        }

        private ListViewItem[] ConvertFromPackArrayToListView(PacketConnectionInfo[] packetConnectionInfos)
        {
            return packetConnectionInfos.Select(CreateListViewItem).ToArray();
        }

        private ListViewItem CreateListViewItem(PacketConnectionInfo packet)
        {
            return new ListViewItem(new string[]
            {
                packet.LocalAddress.ToString(),
                packet.LocalPort.ToString(),
                packet.RemoteAddress.ToString(),
                packet.RemotePort.ToString(),
                packet.MacAddress,
                packet.Protocol.ToString(),
                packet.State.ToString(),
                packet.ProcessId.ToString(),
                NetHelper.GetProcessName((int)packet.ProcessId)
            });
        }

        private void UpdateStatusLabel()
        {
            string statusMessage = $"Total Connections: {listViewConnections.Items.Count}{(connectionsMonitor.IsRunning ? " - Stopped" : " - Scanning ...")}";
            statusStrip.Invoke((Action)(() => toolStripStatusLabel.Text = statusMessage));
        }


        private bool IsPacketFilterMonitor(PacketConnectionInfo packet)
        {
            return ((packet.Protocol == ProtocolType.IPNET || packet.Protocol.HasFlag(ProtocolType.IPNET)) && iPToolStripMenuItem1.Checked) ||
                     ((packet.Protocol == ProtocolType.TCP || packet.Protocol.HasFlag(ProtocolType.TCP)) && tCPToolStripMenuItem.Checked) ||
                     ((packet.Protocol == ProtocolType.UDP || packet.Protocol.HasFlag(ProtocolType.UDP)) && uDPToolStripMenuItem.Checked) ||
                     ((packet.Protocol == ProtocolType.ARP || packet.Protocol.HasFlag(ProtocolType.ARP)) && aRPToolStripMenuItem.Checked) ||
                     ((packet.Protocol == ProtocolType.ICMP || packet.Protocol.HasFlag(ProtocolType.ICMP)) && iCMPToolStripMenuItem.Checked);

        }

        private void tbt_Filter_TextChanged(object sender, EventArgs e)
        {
            string filterText = tbt_Filter.Text.ToLower();

            List<ListViewItem> filteredItems = new List<ListViewItem>();
            foreach (var item in originalItems)
            {
                bool matches = item.SubItems.Cast<ListViewItem.ListViewSubItem>().Any(subItem => subItem.Text.ToLower().Contains(filterText));
                if (matches) filteredItems.Add(item.Clone() as ListViewItem);
            }

            listViewConnections.Items.Clear();
            listViewConnections.Items.AddRange(filteredItems.ToArray());
            UpdateStatusLabel();
        }

        private void UpdateFilter()
        {
            connectionsMonitor.ProtocolFilter = protocolFilters;
        }

        private ProtocolType protocolFilters
        {
            get
            {
                ProtocolType protocolType = ProtocolType.UNKNOWN;
                if (iPToolStripMenuItem1.Checked) protocolType |= ProtocolType.IPNET;
                if (tCPToolStripMenuItem.Checked) protocolType |= ProtocolType.TCP;
                if (uDPToolStripMenuItem.Checked) protocolType |= ProtocolType.UDP;
                if (iCMPToolStripMenuItem.Checked) protocolType |= ProtocolType.ICMP;
                if (dHPCToolStripMenuItem.Checked) protocolType |= ProtocolType.DHCP;
                if (dNSToolStripMenuItem.Checked) protocolType |= ProtocolType.DNS;

                return protocolType;
            }
        }

        private void iPToolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
            isLoadedConnections = false;

            if (connectionsMonitor != null)
            {
                if (iPToolStripMenuItem.Checked)
                {
                    connectionsMonitor.StartListening();
                }
                else
                {
                    connectionsMonitor.StopListening();
                }

                UpdateStatusLabel();
                MessageBox.Show("IP Filter is " + (iPToolStripMenuItem.Checked ? "Enabled" : "Disabled"));
            }
        }

        private void OtherMenuItems_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
            isLoadedConnections = false;
            listViewConnections.Items.Clear();

            //if (connectionsMonitor != null)
            //{
            //    connectionsMonitor.RestartListening();
            //    UpdateStatusLabel();
            //}
        }




        private void pingSnifferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PingForm().Show();
        }

        private void portScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PortScanner_Form().Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void analysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var connections = listViewConnections.Items.Cast<ListViewItem>().Select(item => (ListViewItem)item.Clone()).ToList();

            // Pass the list to the analysisform
            new analysisform(connections).Show();

        }

        private void filterMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolKitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iPToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void iPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void LB_Filter_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dHPCToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void LB_ProductName_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dNSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tCPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void uDPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aRPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iCMPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tab_Home_Click(object sender, EventArgs e)
        {

        }

        private void tab_ListMonitor_Click(object sender, EventArgs e)
        {

        }

        private void listViewConnections_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTcpDescription_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
