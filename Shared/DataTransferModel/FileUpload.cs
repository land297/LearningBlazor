﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DataTransferModel {
    public class FileUpload {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
