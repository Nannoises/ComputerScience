using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public interface INode
    {
        List<INode> Children { get; set; }
        void Visit();
    }
}
