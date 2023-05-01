﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.ImportText.DTOs
{
    public class TransactionsTableDto
    {
        public List<string> AllText { get; set; }
        public List<string> Installments { get; set; }
        public List<string> Date { get; set; }
        public List<string> AmountArs { get; set; }
        public List<string> AmountUsd { get; set; }

        public TransactionsTableDto()
        {
            AllText = new List<string>();
            Installments = new List<string>();
            Date = new List<string>();
            AmountArs = new List<string>();
            AmountUsd = new List<string>();
        }
    }
}
