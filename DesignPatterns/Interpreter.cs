using static System.Console;
namespace Interpreter;

class Context
{
    private string ac_model = "";
    private bool isAircraft = false;
    public Context(string _ac_model) => this.ac_model = _ac_model;

    public string getModel() => this.ac_model;
    public int getLenght() => this.ac_model.Length;
    public string getLastChar() => this.ac_model[this.ac_model.Length - 1].ToString();
    public string getFirstChar() => this.ac_model[0].ToString();
    public void setIsAircraft(bool _isAircraft) => this.isAircraft = _isAircraft;
    public bool getIsAircraft() => isAircraft;
}

interface Expression
{
    void InterpretContext(Context context);
}

class CheckExpression : Expression
{
    public void InterpretContext(Context context)
    {
        //We assume tthe aircraft models only start with A or B and contains 4 or 5 chars.
        string ac_model = context.getModel();
        if (ac_model.StartsWith("A") || ac_model.StartsWith("B"))
        {
            if (ac_model.Length == 4 || ac_model.Length == 5)
            {
                context.setIsAircraft(true);
                WriteLine(ac_model + " is an aircraft...");
            }
            else
            {
                context.setIsAircraft(false);
                WriteLine(ac_model + " is not aircraft...");
            }
        }
        else
        {
            context.setIsAircraft(false);
            WriteLine(ac_model + " is not aircraft...");
        }
    }
}

class BrandExpression : Expression
{
    public void InterpretContext(Context context)
    {
        if (context.getIsAircraft() == true)
        {
            if (context.getFirstChar().Equals("A"))
                WriteLine("Brand is Airbus");
            else if (context.getFirstChar().Equals("B"))
                WriteLine("Brand is Boeing");
        }
        else
            WriteLine("Brand could not be interpreted");
    }
}

class ModelExpression : Expression
{
    public void InterpretContext(Context context)
    {
        if (context.getIsAircraft() == true)
        {
            WriteLine("Model is : " + context.getModel().Substring(1, 3));
        }
        else
            WriteLine("Model could not be interpreted");
    }
}

class TypeExpression : Expression
{
    public void InterpretContext(Context context)
    {
        if (context.getIsAircraft() == true)
        {
            string ac_model = context.getModel();
            if (context.getLenght() == 5 && context.getLastChar().Equals("F"))//F-> Freighter
            {
                WriteLine("Aircraft type is Cargo/Freighter");
            }
            else
                WriteLine("Aircraft type is Passenger Transportation");
        }
        else
            WriteLine("Type could not be interpreted");
    }
}


class App
{
    public void Run()
    {
        Console.Title = "Interpreter Design Pattern Example - TheCodeprogram";

        List<Context> lstAircrafts = new List<Context>();
        List<Expression> lstExpressions = new List<Expression>();

        lstAircrafts.Add(new Context("A330"));
        lstAircrafts.Add(new Context("A330F"));
        lstAircrafts.Add(new Context("B777"));
        lstAircrafts.Add(new Context("B777F"));
        lstAircrafts.Add(new Context("TheCode"));

        lstExpressions.Add(new CheckExpression());
        lstExpressions.Add(new BrandExpression());
        lstExpressions.Add(new ModelExpression());
        lstExpressions.Add(new TypeExpression());

        for (int ac_index = 0; ac_index < lstAircrafts.Count; ac_index++)
        {
            for (int exp_index = 0; exp_index < lstExpressions.Count; exp_index++)
            {
                lstExpressions[exp_index].InterpretContext(lstAircrafts[ac_index]);
            }
            WriteLine("-----------------------------------");
        }
        Console.ReadLine();
    }
}