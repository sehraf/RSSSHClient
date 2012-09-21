using System;
using System.Collections.Generic;

using rsctrl.core;
using rsctrl.files;

namespace RetroShareSSHClient
{
    struct GuiFileTransfer
    {
        FileTransfer _fileTransfer;
        ushort _listIndex;
        bool @new;

        public FileTransfer FileTransfer { get { return _fileTransfer; } set { _fileTransfer = value; } }
        //public File File { get { return _fileTransfer.file; } set { _fileTransfer.file = value; } }
        //public Direction Direction { get { return _fileTransfer.direction; } set { _fileTransfer.direction = value; } }
        //public float Fraction { get { return _fileTransfer.fraction; } set { _fileTransfer.fraction = value; } }
        //public float Speed { get { return _fileTransfer.rate_kBs; } set { _fileTransfer.rate_kBs = value; } }
        public string Hash { get { return _fileTransfer.file.hash; } set { _fileTransfer.file.hash = value; } }
        public ushort Index { get { return _listIndex; } set { _listIndex = value; } }
        public bool @New { get { return @new; } set { @new = value; } }

        public bool Download { get { return (_fileTransfer.direction == rsctrl.files.Direction.DIRECTION_DOWNLOAD); } }
        public bool Upload { get { return (_fileTransfer.direction == rsctrl.files.Direction.DIRECTION_UPLOAD); } }
    }

    class GuiFileTransferComparer : IComparer<GuiFileTransfer>
    {
        public int Compare(GuiFileTransfer p1, GuiFileTransfer p2)
        {
            return p1.FileTransfer.file.name.CompareTo(p2.FileTransfer.file.name);
        }
    }

    class FileProcessor
    {
        const bool DEBUG_FILES = false;   

        Dictionary<string, GuiFileTransfer> _fileTransfers;

        Bridge _b;

        public FileProcessor(Bridge bridge)
        {
            _b = bridge;

            _fileTransfers = new Dictionary<string, GuiFileTransfer>();
        }

        public void Reset()
        {
            _fileTransfers.Clear();
            _b.GUI.lb_filesDownloads.Items.Clear();
            _b.GUI.lb_filesUploads.Items.Clear();
        }

        public void UpdateFileTransfers(ResponseTransferList list)
        {
            if (list.transfers.Count == 0)
                return;
            List<string> hashes = new List<string>();
            Direction dir = list.transfers[0].direction;
            bool added = false;
            bool removed = false;

            // add new / update old
            foreach (FileTransfer ft in list.transfers)
            {
                if (ft == null)
                    continue;
                if (_fileTransfers.ContainsKey(ft.file.hash)) // update
                {
                    GuiFileTransfer gft = _fileTransfers[ft.file.hash];
                    gft.FileTransfer = ft;
                    _fileTransfers[ft.file.hash] = gft;
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_FILES, "FileTransfer: updating " + ft.file.hash);
                }
                else //new
                {
                    GuiFileTransfer gft = new GuiFileTransfer();
                    gft.FileTransfer = ft;
                    gft.New = true;
                    _fileTransfers.Add(gft.Hash, gft);
                    added = true;
                    System.Diagnostics.Debug.WriteLineIf(DEBUG_FILES, "FileTransfer: adding " + ft.file.hash);
                }
                hashes.Add(ft.file.hash);
            }

            // remove
            GuiFileTransfer[] fileTransfers = new GuiFileTransfer[_fileTransfers.Values.Count];
            _fileTransfers.Values.CopyTo(fileTransfers, 0);
            foreach (GuiFileTransfer gft in fileTransfers)
            {
                if (!hashes.Contains(gft.Hash) && gft.FileTransfer.direction == dir)
                {
                    _fileTransfers.Remove(gft.Hash);
                    removed = true;
                }
            }

            UpdateFileLists(dir, added, removed);
        }

        public void UpdateFileLists(Direction dir, bool added, bool removed)
        {
            List<GuiFileTransfer> tmpList = new List<GuiFileTransfer>();
            GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];

            _fileTransfers.Values.CopyTo(values, 0);
            foreach (GuiFileTransfer gft in values)
            {
                if (gft.FileTransfer.direction == dir)
                    tmpList.Add(gft);
            }

            List<GuiFileTransfer> list = new List<GuiFileTransfer>();

            Dictionary<string, GuiFileTransfer>.ValueCollection fileTransfers = _fileTransfers.Values;
            foreach (GuiFileTransfer gft in fileTransfers)
                if (dir == gft.FileTransfer.direction)
                    list.Add(gft);

            list.Sort(new GuiFileTransferComparer());

            string hash = "";
            GuiFileTransfer tmpGft;
            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                if (_b.GUI.lb_filesDownloads.SelectedIndex != -1)
                    if (GetFileTransferByListIndexDir(_b.GUI.lb_filesDownloads.SelectedIndex, dir, out tmpGft))
                        hash = tmpGft.Hash;
            }
            else
            {
                if (_b.GUI.lb_filesUploads.SelectedIndex != -1)
                    if (GetFileTransferByListIndexDir(_b.GUI.lb_filesUploads.SelectedIndex, dir, out tmpGft))
                        hash = tmpGft.Hash;
            }

            string s;
            ushort index = 0;
            int selectedItem = -1;
            GuiFileTransfer gft2;
            string[] list2 = new string[list.Count];

            foreach (GuiFileTransfer gft in list)
            {
                s = BuildFileTransferString(gft.FileTransfer);
                list2[index] = s;

                gft2 = _fileTransfers[gft.Hash];
                gft2.Index = index;
                _fileTransfers[gft.Hash] = gft2;

                if (hash == gft.Hash)
                    selectedItem = index;

                index++;
            }

            if (dir == Direction.DIRECTION_DOWNLOAD)
            {
                _b.GUI.lb_filesDownloads.Items.Clear();
                _b.GUI.lb_filesDownloads.Items.AddRange(list2);
                _b.GUI.lb_filesDownloads.SelectedIndex = selectedItem;
            }
            else
            {
                _b.GUI.lb_filesUploads.Items.Clear();
                _b.GUI.lb_filesUploads.Items.AddRange(list2);
                _b.GUI.lb_filesUploads.SelectedIndex = selectedItem;
            }

        }

        private string BuildFileTransferString(FileTransfer ft)
        {
            return String.Format("{0:0,0.00}", ft.rate_kBs) + "kBs - " +
                ((ft.direction == Direction.DIRECTION_DOWNLOAD) ? (String.Format("{0:0,0.00}", ft.fraction * 100) + "% - ") : "") +
                Processor.BuildSizeString(ft.file.size) + " - " +
                ft.file.name;
        }

        public bool GetFileTransferBySelection(out GuiFileTransfer gft)
        {
            gft = new GuiFileTransfer();
            if (_b.GUI.lb_filesDownloads.SelectedIndex != -1)
                if (GetFileTransferByListIndexDir(_b.GUI.lb_filesDownloads.SelectedIndex, Direction.DIRECTION_DOWNLOAD, out gft)) ;
                else return false;
            else if (_b.GUI.lb_filesUploads.SelectedIndex != -1)
                if (GetFileTransferByListIndexDir(_b.GUI.lb_filesUploads.SelectedIndex, Direction.DIRECTION_UPLOAD, out gft)) ;
                else return false;
            else return false;
            return true;
        }

        private bool GetFileTransferByListIndexDir(int index, Direction dir, out GuiFileTransfer gft)
        {
            GuiFileTransfer[] values = new GuiFileTransfer[_fileTransfers.Values.Count];
            _fileTransfers.Values.CopyTo(values, 0);
            foreach (GuiFileTransfer ft in values)
            {
                if (ft.Index == index && ft.FileTransfer.direction == dir)
                {
                    gft = ft;
                    return true;
                }
            }
            gft = default(GuiFileTransfer);
            return false;
        }

    }
}
