using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApplication.Models;

namespace MyWebApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Receipts.Any())
            {
                return;
            }
            var branches = new Branch[]
            {
                new Branch{ Address="RICHMOND HILL,1650 ELGIN MILLS RD EAST, SUITE 401, ON,L4S 0B2", BranchID = 1},
                new Branch{ Address="SCARBOROUGH,3290 MIDLAND AVENUE, UNIT 6, ON, M1V 3Z9", BranchID = 2},
                new Branch{ Address="MONTREAL, 1111 ST-URBAIN, BUREAU 112, QC, H2Z 1Y6",BranchID = 3},
                new Branch{ Address="VANCOUVER, 139 KEEFER STREET, SUITE 202, BC, V6A 1X3", BranchID =4}

            };
            foreach (Branch d in branches)
            {
                context.Branches.Add(d);
            }
            context.SaveChanges();
            var receipts = new Cash[]
            {
                new Cash{Amount=500,ClientName="QIYOUJI",Remarks="INV58011460",Payment= Payment.Bank, CurrencyCode = CurrencyCode.CAD, BranchID = branches.Single(s => s.Address == "RICHMOND HILL,1650 ELGIN MILLS RD EAST, SUITE 401, ON,L4S 0B2").BranchID },
                new Cash{Amount=150,ClientName="CTRIP",Remarks="INV18015460",Payment= Payment.Bank, CurrencyCode = CurrencyCode.CAD, BranchID = branches.Single(s => s.Address == "SCARBOROUGH,3290 MIDLAND AVENUE, UNIT 6, ON, M1V 3Z9").BranchID },
                new Cash{Amount=154500,ClientName="YONGSHUN",Remarks="INV16011460",Payment= Payment.Cash, CurrencyCode = CurrencyCode.CAD, BranchID = branches.Single(s => s.Address == "MONTREAL, 1111 ST-URBAIN, BUREAU 112, QC, H2Z 1Y6").BranchID },
                new Cash{Amount=1500,ClientName="LUTAO",Remarks="INV18311460",Payment= Payment.Bank, CurrencyCode = CurrencyCode.USD, BranchID = branches.Single(s => s.Address == "VANCOUVER, 139 KEEFER STREET, SUITE 202, BC, V6A 1X3").BranchID }
            };
            foreach (Cash d in receipts)
            {
                context.Receipts.Add(d);
            }
            context.SaveChanges();
        
        }
        
    }
}
