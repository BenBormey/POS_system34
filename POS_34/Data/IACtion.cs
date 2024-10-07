using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.view
{
    public  interface IACtion
    {
        void create(DataGridView dg);
        void update(DataGridView dg);
        void delete(DataGridView dg);
        void search(DataGridView dg);
        void loadData(DataGridView dg);
    }
}
