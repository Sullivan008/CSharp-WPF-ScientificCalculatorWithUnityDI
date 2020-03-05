using Calculator.ViewModels.MainWindow;
using System.Threading.Tasks;

namespace Calculator.Modules.Calculator.Interfaces
{
    public interface IBasicCalculatorHandler
    {
        CalculatorViewModel Model { get; set; }

        /// <summary>
        ///     Számjegy lenyomásához tartozó metódus. Ez a metódus hívódik meg akkor is, ha
        ///     a felhasználó tizedes vesszőt nyom le.
        /// </summary>
        /// <param name="numberText">
        ///     XAML felől kapott Command paraméter. 
        ///     A számok 0-9-ig. Vagy egy tizedesvessző (,).
        /// </param>
        void NumberButtonOperation(object numberText);

        /// <summary>
        ///     Az alapvető műveleti jel lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="arithmeticOperatorText">
        ///     XAML felől kapott Command paraméter: +, -, *, /
        /// </param>
        void BasicArithmeticOperatorButtonOperation(object arithmeticOperatorText);

        /// <summary>
        ///     Az ANS gomb lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="ansText">XAML felől kapott Command paraméter= Ans.</param>
        void AnsButtonOperation(object ansText);

        /// <summary>
        ///     A számítás végrehajtását végrehajtó metódus.
        /// </summary>
        /// <param name="equalText">XAML felől kapott Command paraméter: =</param>
        void EqualButtonOperation(object equalText);

        /// <summary>
        ///     A DEL gomb lenyomásához tartozó metódus. Visszatörlünk egy karaktert,
        ///     az INPUT Beviteli mezőből.
        /// </summary>
        Task DelButtonOperation();

        /// <summary>
        ///     Az AC gomb lenyomásához tartozó metódus. Minden Property-t az alapértelmezett értékre
        ///     állítunk.
        /// </summary>
        Task AcButtonOperation();

        /// <summary>
        ///     A bal zárójel gomb lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="leftParenthesisText">XAML felől kapott Command paraméter: (.</param>
        void LeftParButtonOperation(object leftParenthesisText);

        /// <summary>
        ///     A jobb zárójel gomb lenyomásához tartozó metódus.
        /// </summary>
        /// <param name="rightParenthesisText">XAML felől kapott Command paraméter: ).</param>
        void RightParButtonOperation(object rightParenthesisText);

        /// <summary>
        ///     A hatványozás lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="powFunctionText">XAML felől kapott Command paraméter: ^</param>
        void PowButtonOperation(object powFunctionText);

        /// <summary>
        ///     A négyzetes hatványozás lenyomásához tartozó gomb.
        /// </summary>
        Task SquareButtonOperation();

        /// <summary>
        ///     A szögfüggvények lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="trigonometricFunctionText">
        ///     XAML felől kapott Command paraméter: Sin, InvSin, Cos, InvCos, Tan, InvTan, sqrt.
        /// </param>
        void TrigonometricFunctionsButtonOperation(object trigonometricFunctionText);

        /// <summary>
        ///     A maradékos osztás lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="modFunctionText">XAML felől kapott Command paraméter: %</param>
        void ModButtonOperation(object modFunctionText);

        /// <summary>
        ///     Az Ln és Log lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: ln, log, eⁿ, 10ⁿ</param>
        void LnLogButtonOperation(object paramData);

        /// <summary>
        ///     Az N. Gyök alatt az X lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="paramData">XAML felől kapott Command paraméter: ln, log, eⁿ, 10ⁿ</param>
        void RootButtonOperation(object paramData);

        /// <summary>
        ///     Az E lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="eFunctionText">XAML felől kapott Command paraméter: e</param>
        void EButtonOperation(object eFunctionText);

        /// <summary>
        ///     A Fact lenyomásához tartozó gomb.
        /// </summary>
        /// <param name="factFunctionText">XAML felől kapott Command paraméter: !</param>
        void FactButtonOperation(object factFunctionText);

        /// <summary>
        ///     A ± lenyomásához tartozó gomb.
        /// </summary>
        Task PlusMinusButtonOperation();
    }
}
