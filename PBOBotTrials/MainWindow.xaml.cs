using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Threading;
using PBOBotTrials.Helpers;
using System.Threading;

namespace PBOBotTrials
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {      
        private LowLevelKeyboardListener _listener;
        CancellationTokenSource source = new CancellationTokenSource();
        
        CancellationToken cancellationToken = new CancellationToken();

        public MainWindow()
        {
            InitializeComponent();
            var token = source.Token;
        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;

            _listener.HookKeyboard();
        }
        async void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            var keyPressed = e.KeyPressed.ToString();
            if(keyPressed == "B" || keyPressed == "b")
            {
                await Delay(3000);                
                source = new CancellationTokenSource();
                try
                {
                    Fishing(source);
                }
                catch(OperationCanceledException)
                {

                }
                
            }
            if(keyPressed == "N" || keyPressed == "n")
            {
                source.Cancel();
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _listener.UnHookKeyboard();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {           
            await Delay(10000);          
            Fishing(source);
        }     
        
        async void Fishing(CancellationTokenSource sourceToken)
        {            
            var cToken = sourceToken.Token;
            bool repeat = true;
            bool infinite = true;
            int actionDelay = 1400;            
            for (int i = 0; i < Int16.Parse(S1PP.Text.ToString()); i++)
            {                
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(actionDelay);
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(1000);
                FightWithSkill("1");
                await Task.Delay(actionDelay);
                cancellationTokenCheck();
            }

            for(int i = 0; i < Int16.Parse(S2PP.Text.ToString()); i++)
            {
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(actionDelay);
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(1000);
                FightWithSkill("2");
                await Task.Delay(actionDelay);
                if (cToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }
            for (int i = 0; i < Int16.Parse(S3PP.Text.ToString()); i++)
            {
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(actionDelay);
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(1000);
                FightWithSkill("3");
                await Task.Delay(actionDelay);

            }
            for (int i = 0; i < Int16.Parse(S4PP.Text.ToString()); i++)
            {
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(actionDelay);
                System.Windows.Forms.SendKeys.SendWait("F");
                await Task.Delay(1000);
                FightWithSkill("4");
                await Task.Delay(actionDelay);
            }

            if(repeat == true)
            {
                int j = 20;
                while(j>0 || infinite==true)
                {
                    System.Windows.Forms.SendKeys.SendWait("F");
                    await Task.Delay(actionDelay);
                    System.Windows.Forms.SendKeys.SendWait("F");
                    await Task.Delay(1000);
                    if(Int16.Parse(S1PP.Text.ToString())>0)
                    {
                        await Task.Delay(200);
                        FightWithSkill("1");
                    }                       
                    if (Int16.Parse(S2PP.Text.ToString()) > 0)
                    {
                        await Task.Delay(200);
                        FightWithSkill("2");
                    }                        
                    if (Int16.Parse(S3PP.Text.ToString()) > 0)
                    {
                        await Task.Delay(200);
                        FightWithSkill("3");
                    }                        
                    if (Int16.Parse(S4PP.Text.ToString()) > 0)
                    {
                        await Task.Delay(200);
                        FightWithSkill("4");
                    }                        
                    await Task.Delay(actionDelay);
                    j--;
                }
            }

            void cancellationTokenCheck()
            {
                try
                {
                    cToken.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException)
                {
                    // throw;
                    System.Windows.MessageBox.Show("Task cancel");
                }
                finally
                {

                }
            }
        }

        public void FightWithSkill(string skillNumber)
        {            
            System.Windows.Forms.SendKeys.SendWait(skillNumber);
        }

        async Task Delay(int miliseconds)
        {
            await Task.Delay(miliseconds);
        }     
    }
}
