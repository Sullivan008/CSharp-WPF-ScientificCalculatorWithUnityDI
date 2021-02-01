namespace Calculator.Views.Calculator.Enums
{
    public enum UserInteractionType
    {
        Unknown = 0,

        #region OPERATIONS

        AcBtnPressed = 1,
        DelBtnPressed = 2,
        AnsBtnPressed = 3,
        CommaBtnPressed = 4,
        EqualBtnPressed = 5,
        OperandBtnPressed = 6,
        PlusMinusBtnPressed = 7,
        EToTheNthPowBtnPressed = 8,
        TenToTheNthPowBtnPressed = 9,

        #endregion

        #region OPERATORS

        BasicArithmeticOperatorBtnPressed = 10,
        ModOperatorBtnPressed = 11,
        PowOperatorBtnPressed = 12,
        SquareOfXNumberBtnPressed = 13,
        FactOperatorBtnPressed = 14,
        LeftParenthesisBtnPressed = 15,
        RightParenthesisBtnPressed = 16,

        #endregion

        #region TRIGONOMETRIC FUNCTIONS

        SinFunctionBtnPressed = 17,
        CosFunctionBtnPressed = 18,
        TanFunctionBtnPressed = 19,
        ArcSinFunctionBtnPressed = 20,
        ArcCosFunctionBtnPressed = 21,
        ArcTanFunctionBtnPressed = 22,

        #endregion

        #region FUNCTIONS

        SqrtFunctionBtnPressed = 23,
        YRootFunctionBtnPressed = 24,
        EFunctionBtnPressed = 25,
        LnFunctionBtnPressed = 26,
        LogFunctionBtnPressed = 27,

        #endregion
    }
}
