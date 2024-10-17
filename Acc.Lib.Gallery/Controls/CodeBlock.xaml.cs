using System;
using System.Windows.Controls;
using ColorCode;
using ColorCode.Wpf;

namespace Acc.Lib.Gallery.Controls;

public partial class CodeBlock : UserControl
{
    public static readonly DependencyProperty CodeProperty = DependencyProperty.Register(
        nameof(Code),
        typeof(string),
        typeof(CodeBlock),
        new PropertyMetadata(default(string), HandlePropertyChanged));

    public string Code
    {
        get => (string)this.GetValue(CodeProperty);
        set => this.SetValue(CodeProperty, value);
    }

    public static readonly DependencyProperty CodeLanguageProperty = DependencyProperty.Register(
        nameof(CodeLanguage),
        typeof(CodeBlockLanguage),
        typeof(CodeBlock),
        new PropertyMetadata(default(CodeBlockLanguage), HandlePropertyChanged));

    public CodeBlockLanguage CodeLanguage
    {
        get => (CodeBlockLanguage)this.GetValue(CodeLanguageProperty);
        set => this.SetValue(CodeLanguageProperty, value);
    }

    public CodeBlock()
    {
        this.InitializeComponent();
    }

    private static ILanguage GetColorCodeLanguage(CodeBlockLanguage codeBlockCodeLanguage)
    {
        return codeBlockCodeLanguage switch
        {
            CodeBlockLanguage.Asax => Languages.Asax,
            CodeBlockLanguage.Ashx => Languages.Ashx,
            CodeBlockLanguage.Aspx => Languages.Aspx,
            CodeBlockLanguage.AspxCs => Languages.AspxCs,
            CodeBlockLanguage.AspxVb => Languages.AspxVb,
            CodeBlockLanguage.CSharp => Languages.CSharp,
            CodeBlockLanguage.Cpp => Languages.Cpp,
            CodeBlockLanguage.Css => Languages.Css,
            CodeBlockLanguage.Fortran => Languages.Fortran,
            CodeBlockLanguage.FSharp => Languages.FSharp,
            CodeBlockLanguage.Haskell => Languages.Haskell,
            CodeBlockLanguage.Html => Languages.Html,
            CodeBlockLanguage.Java => Languages.Java,
            CodeBlockLanguage.JavaScript => Languages.JavaScript,
            CodeBlockLanguage.Json => Languages.FindById("json"),
            CodeBlockLanguage.Koka => Languages.Koka,
            CodeBlockLanguage.Markdown => Languages.Markdown,
            CodeBlockLanguage.MatLab => Languages.MATLAB,
            CodeBlockLanguage.Php => Languages.Php,
            CodeBlockLanguage.PowerShell => Languages.PowerShell,
            CodeBlockLanguage.Python => Languages.Python,
            CodeBlockLanguage.Sql => Languages.Sql,
            CodeBlockLanguage.TypeScript => Languages.Typescript,
            CodeBlockLanguage.VbDotNet => Languages.VbDotNet,
            _ => Languages.Xml
        };
    }

    private static void HandlePropertyChanged(DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs eventArgs)
    {
        var codeBlock = dependencyObject as CodeBlock;
        var language = GetColorCodeLanguage(codeBlock!.CodeLanguage);
        var formatter = new RichTextBoxFormatter();
        formatter.FormatRichTextBox(codeBlock.Code, language, codeBlock.RichTextBox);
        return;
    }
}