using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAccess.SqlServerTest
{
    public static class DbConfiguration
    {
        public static string DbInitialScript => @"

            ALTER SEQUENCE [dbo].[Seq_Categories] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_Branches] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_Attachments] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_People] RESTART WITH 6 ;  
            DELETE Categories;
            DELETE Branches;
            DELETE Attachments;
            DELETE People;

            Insert INTO Categories VALUES(1, 'CATE 1', 'SOME Cate');
            Insert INTO Categories VALUES(2, 'CATE 2', 'SOME Cate');
            Insert INTO Categories VALUES(3, 'CATE 3', 'SOME desc');

            Insert INTO Attachments VALUES(1, 1, 'SOME Attachments');
            Insert INTO Attachments VALUES(2, 2, 'SOME Attachments');
            Insert INTO Attachments VALUES(3, 3, 'SOME Attachments');

            Insert INTO Branches VALUES(1, 1, 'Madina', null,null, null);
            Insert INTO Branches VALUES(2, 1, 'Makkah', null,null, null);
            Insert INTO Branches VALUES(3, 2, 'Karachi', null,null, null);

            Insert INTO People VALUES(1, 'Ahmed', '00000');
            Insert INTO People VALUES(2, 'Muhammad', '000000');
            Insert INTO People VALUES(3, 'Shehriyar', '00000');

";
    }
}
