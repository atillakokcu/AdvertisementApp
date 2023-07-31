using AdvertisementApp.Dto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Dto;
    public class AdvertisementUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool Status { get; set; }

        public string Descrpition { get; set; }


    }
