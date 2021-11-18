using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace CargoTrackingSystem

{

    class Koordinatlar
    {
        //[FirestoreProperty]

        public string lat { get; set; }

        //[FirestoreProperty]

        public string lng { get; set; }
    }
}