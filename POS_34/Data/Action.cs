using POS.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Data
{
    public abstract class Action : IACtion
    {
        public virtual void create(DataGridView dg)
        {

        }

        public virtual void delete(DataGridView dg)
        {
            throw new NotImplementedException();
        }

        public virtual void loadData(DataGridView dg)
        {
            throw new NotImplementedException();
        }

        public virtual void search(DataGridView dg)
        {
            throw new NotImplementedException();
        }

        public virtual void update(DataGridView dg)
        {
            throw new NotImplementedException();
        }
    }
}
