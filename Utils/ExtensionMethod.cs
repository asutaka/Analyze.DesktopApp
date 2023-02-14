using Analyze.DesktopApp.Models;
using AutoMapper;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Newtonsoft.Json;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Analyze.DesktopApp.Common
{
    public static class ExtensionMethod
    {
        public static Mapper MapperConfig = new Mapper(new MapperConfiguration(config => { config.AddProfile(new MapProfile()); }));

        public static T LoadJsonFile<T>(this T val, string fileName)
        {
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}\\settings\\{fileName}";
                if (!File.Exists(path))
                    return default(T); 
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
            }
            catch(Exception ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod.LoadJsonFile|EXCEPTION|INPUT: fileName: {fileName}| {ex.Message}");
                return default(T);
            }
        }

        public static bool UpdateJson<T>(this T _model, string fileName)
        {
            try
            {
                string path = $"{Directory.GetCurrentDirectory()}\\settings\\{fileName}";
                var check = File.Exists(path);
                if(!check)
                {
                    using (StreamWriter w = File.AppendText(path)) ;
                }
                string json = JsonConvert.SerializeObject(_model);
                //write string to file
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod.UpdateJson|EXCEPTION|INPUT: fileName: {fileName}| {ex.Message}");
                return false;
            }
        }

        public static string PhoneFormat(this string input, bool isCharacter = true)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            var region = string.Empty;
            if (input.Substring(0, 1) != "+")
                region = "VN";
            var val = GetPhone(input, region);
            return isCharacter ? val.Item2 : val.Item2.Replace("+", "");
        }

        private static (int, string) GetPhone(string input, string region = "")
        {
            try
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                if (input.Length >= 9 && input.Length <= 20)
                {
                    var numberProto = phoneUtil.Parse(input.Trim(), region);
                    int countryCode = numberProto.CountryCode;
                    var formattedPhone = phoneUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL).Replace(" ", "");
                    return (countryCode, formattedPhone);
                }
                return (0, string.Empty);
            }
            catch (NumberParseException ex)
            {
                NLogLogger.PublishException(ex, $"ExtensionMethod.GetPhone|EXCEPTION|INPUT: input: {input}| {ex.Message}");
                return (0, string.Empty);
            }
        }

        public static void AddLine(this RichTextBox box, string text, uint? maxLine = null)
        {
            string newLineIndicator = "\n";

            /*max line check*/
            if (maxLine != null && maxLine > 0)
            {
                if (box.Lines.Count() >= maxLine)
                {
                    List<string> lines = box.Lines.ToList();
                    lines.RemoveAt(0);
                    box.Lines = lines.ToArray();
                }
            }

            /*add text*/
            string line = String.IsNullOrEmpty(box.Text) ? text : newLineIndicator + text;
            box.AppendText(line);
        }

        public static DateTime UnixTimeStampToDateTime(this int unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            try
            {
                return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T To<T>(this TicketModel model)
        {
            if (model == null)
                return default(T);
            return MapperConfig.Map<T>(model);
        }

        public static T To<T>(this List<TicketModel> model)
        {
            if (model == null)
                return default(T);
            return MapperConfig.Map<T>(model);
        }

        public static string To2Digit(this int val)
        {
            if (val > 9)
                return val.ToString();
            return $"0{val}";
        }

        public static void AddTab(this XtraTabControl tabControl, XtraForm form)
        {
            var tp = tabControl.TabPages.FirstOrDefault(x => x.Name == form.Name);
            if (tp != null)
            {
                tabControl.SelectedTabPage = tp;
                return;
            }
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Visible = true;

            var TAbAdd = new XtraTabPage();
            TAbAdd.Text = form.Text;
            TAbAdd.Name = form.Name;
            TAbAdd.Controls.Add(form);
            form.Dock = DockStyle.Fill;

            tabControl.TabPages.Add(TAbAdd);
            tabControl.SelectedTabPage = TAbAdd;
        }

        public static void AddControl(this Panel pnl, XtraForm form)
        {
            pnl.Controls.Clear();
            
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Visible = true;

            pnl.Controls.Add(form);
            form.Dock = DockStyle.Fill;
        }

        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<TicketModel, API24hVM>()
                .ForMember(dest => dest.Coin, opt => opt.MapFrom(src => src.symbol));
        }
    }
}
