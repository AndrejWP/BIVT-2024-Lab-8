using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class White
{
    private string _input;
    public string Input => _input;

    protected White(string input)
    {
        _input = input;
    }

    public abstract void Review();
}