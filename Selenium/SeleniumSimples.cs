// 1- Nomespace ~ Pacote ~ Grupo de Class ~ Workspace
namespace SeleniumSimples;

using System.Xml;
// 2 - Bibliotecas - Depêndencias
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; // alterei ~
using OpenQA.Selenium.DevTools.V118.IndexedDB;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

// 3 - Classe
[TestFixture] // Configura como uma classe de teste
public class AdicionarProdutoNoCarrinhoTest {

/// 3.1 - Atributos ~ Características ~ Campos 
private  IWebDriver driver; // objeto do Selenium WebDriver
    
// 3.2 - Função ou Métodos de Apoio
// Função de Leitura do arquivo csv - massa de teste

public static IEnumerable<TestCaseData> lerDadosDeTeste()
{
    // declaramos um objeto chamado reader que lê o conteúdo do csv
    using (var reader = new StreamReader(@"C:\Iterasys\Loja139\data\login.csv"))
     {
        // pular a linha do cabeçalho do csv
        reader.ReadLine();
        
        
        // Faça enquanto  não   for o final do arquivo
        // -- while --    -!- -- reader.EndOfStream   
             while (!reader.EndOfStream)

            {
            // Vai ler a linha correspondente 
                var linha = reader.ReadLine();
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
                var valores = linha.Split(", ");
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

                yield return new TestCaseData(valores[0], valores[1], valores[2]);     

            } // fim do while - funciona como uma mola

     };



}





// 3.3 - Configurações Antes do teste
[SetUp] // Configura um método para ser executado antes dos testes
public void Before (){ // Configura um metódo antes do teste
    // Faz o download e instalação da versão mais recente do ChromeDriver
    new DriverManager().SetUpDriver(new ChromeConfig());
    driver = new ChromeDriver(); // Instancia o objeto do Selenium com o Chrome
    driver.Manage().Window.Maximize(); // maximiza a janela do navegador
    // Configura uma espera de 5 segundos para qualquer elemento aparecer
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5000);
} // fim do Before

// 3.4 - Configurações depois do teste
[TearDown] // Configura um método para ser usado depois dos testes
public void After (){
    driver.Quit(); // Destruir o objeto do Selenium na memória
} // fim do after
// 3.5 - O(s) Teste(s)

[Test] // Indica que é um método de teste
public void Login()   {
    // abrir o navegador e acessar o site
    driver.Navigate().GoToUrl("https://www.saucedemo.com");
    
    // preencher  o usurario
    driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
    
    // preencher a senha
    driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
    
    //Clicar no botão de login
    driver.FindElement(By.CssSelector("input.submit-button.btn_action")).Click();
   
   // verificar se fizemos o login no site
    Assert.That(driver.FindElement(By.CssSelector("span.title")).Text, Is.EqualTo("Products"));


} // fim do Teste de login

 
[Test, TestCaseSource(nameof(lerDadosDeTeste))] // Indica que é um método de teste
public void LoginTestDDT(String usurario, String senha, String resultadoEsperado){
    // abrir o navegador e acessar o site
    driver.Navigate().GoToUrl("https://www.saucedemo.com");
    
    // preencher  o usurario
    driver.FindElement(By.Id("user-name")).SendKeys(usurario);
    
    // preencher a senha
    driver.FindElement(By.Id("password")).SendKeys(senha);
    
    //Clicar no botão de login
    driver.FindElement(By.CssSelector("input.submit-button.btn_action")).Click();
   
   // verificar se fizemos o login no site
    Assert.That(driver.FindElement(By.CssSelector("span.title")).Text, Is.EqualTo(resultadoEsperado));









} // fim da classe

}