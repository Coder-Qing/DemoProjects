using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        public static double Num { get; set; } =0.00;

        static void Main(string[] args)
        {
            //while (true)
            //{
            //    string s = "新建文本文档 (3).txt";
            //    Console.WriteLine(Path.GetFileName(s));

            //    //Console.Write("请输入文件大小：");
            //    //if (long.TryParse(Console.ReadLine(), out long length))
            //    //{
            //    //    Console.WriteLine(FileLengthConversion(length));
            //    //}
            //    //else
            //    //{
            //    //    Console.WriteLine("您输入的不是数字");
            //    //}


            //    //$"{Math.Ceiling(_fileContent.FileSize.Value / 1024d)}KB"
            //}

            //string key = "banglin1";
            //string i = Des.Encrypt("333333", key).ToString();
            //Console.WriteLine(i);

            //Console.WriteLine(Des.DesDecrypt(i,key));



            //GetAssemblyInfo();
            //GetTypeInfo();

            #region 普通反射


            #region 方法1
            ////加载程序集
            //Assembly assembly = Assembly.Load("ConsoleDemo");
            ////获取对象
            //Type type = assembly.GetType("ConsoleDemo.ReflectDemoClass");
            ////使用指定类型的默认构造函数来创建该类型的实例。
            //object obj = System.Activator.CreateInstance(type);
            ////获取类中的方法
            //MethodInfo method = type.GetMethod("PrintText");
            ////执行方法 传入对象
            //method.Invoke(obj, null);
            #endregion


            #region 方法2


            //object obj2 = Assembly.Load("ConsoleDemo").CreateInstance("ConsoleDemo.ReflectDemoClass");
            ////获取当前实例的type
            //Type type2 = obj2.GetType();
            //MethodInfo methodInfo2 = type2.GetMethod("PrintText");
            //methodInfo2.Invoke(obj2, null);
            #endregion

            #endregion

            #region 运用工厂模式的反射

            #region 方法一

            //AbsFruit absFruit = FruitFactor.CreateInstance<AbsFruit>("ConsoleDemo", "Strawberry");
            //absFruit.Show();
            #endregion


            #region 方法二

            //string fullTypeName = typeof(Strawberry).AssemblyQualifiedName;

            //AbsFruit absFruit2 = FruitFactor.CreateInstance<AbsFruit>(fullTypeName);
            //absFruit2.Show();

            #endregion
            #endregion


            string str = "," +
                "";
            Console.WriteLine((Num * 100 / 100.00).ToString("0.00"));



            Console.ReadLine();

        }


        /// <summary>
        /// 获取程序集的信息
        /// </summary>
        public static void GetAssemblyInfo()
        {

            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine("程序集全名：{0}", assembly.FullName);
            Console.WriteLine("程序集的版本：{0}", assembly.GetName().Version);
            Console.WriteLine("程序集初始位置：{0}", assembly.CodeBase);
            Console.WriteLine("程序集位置：{0}", assembly.Location);
            Console.WriteLine("程序集入口：{0}", assembly.EntryPoint);
            Type[] types = assembly.GetTypes();
            Console.WriteLine("程序集中包含的类型：");
            foreach (Type item in types)
            {
                Console.WriteLine("类：" + item.Name);
            }


        }

        /// <summary>
        /// 使用反射获取
        /// </summary>
        public static void GetTypeInfo()
        {
            Type type = typeof(Des);
            Console.WriteLine("类型名：{0}", type.Name);
            Console.WriteLine("类全名：{0}", type.FullName);
            Console.WriteLine("命名空间：{0}", type.Namespace);
            Console.WriteLine("程序集名：{0}", type.Assembly);
            Console.WriteLine("模块名：{0}", type.Module);
            Console.WriteLine("基类名：{0}", type.BaseType);
            Console.WriteLine("是否类：{0}", type.IsClass);
            Console.WriteLine("类的公共成员：");
            MemberInfo[] members = type.GetMembers();
            foreach (MemberInfo memberInfo in members)
            {
                Console.WriteLine("{0}:{1}", memberInfo.MemberType, memberInfo);
            }
        }




        // public static byte[] encrypt(String message, String key) throws Exception
        // {
        //     Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
        //     DESKeySpec desKeySpec = new DESKeySpec(key.getBytes("UTF-8"));
        //     SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("DES");
        // SecretKey secretKey = keyFactory.generateSecret(desKeySpec);
        // IvParameterSpec iv = new IvParameterSpec(key.getBytes("UTF-8"));
        // cipher.init(Cipher.ENCRYPT_MODE, secretKey, iv);
        // return cipher.doFinal(message.getBytes("UTF-8"));   
        //}

        public static string FileLengthConversion(long length)
        {
            int conversionLengthSquare = 0;
            string conversionString = "BYTE";
            if (length >= 0 && length < 1024)
            {
            }
            else if (length >= 1024 && length < 1048576)
            {
                conversionLengthSquare = 1;
                conversionString = "KB";
            }
            else if (length >= 1048576 && length < 1073741824)
            {
                conversionLengthSquare = 2;
                conversionString = "MB";
            }
            else if (length >= 1073741824 && length < 1099511627776)
            {
                conversionLengthSquare = 3;
                conversionString = "GB";
            }

            if (conversionLengthSquare == 0)
            {
                return $"{length}{conversionString}";
            }
            else if (conversionLengthSquare >= 3)
            {
                return $"{length / Math.Pow(1024, conversionLengthSquare):f2}{conversionString}";
            }
            return $"{Math.Ceiling(length / Math.Pow(1024, conversionLengthSquare))}{conversionString}";
        }



    }


    public static class XiaoYuCodeHelper
    {
        public static Dictionary<string, string> XYReasionDic = new Dictionary<string, string>()
        {
            {"CHARGE,644","没有呼叫权限"},
            {"CHARGE,649","预约会议未到开始时间，无法主动加入"},
            {"SIG,704","UNKONWN"},
            {"SIG,703","UNKONWN"},
            {"SIG,708","没有白板分享权限"},
            {"SIG,709","不能合并会议"},
            {"SIG,706","没有内容标注权限"},
            {"SIG,707","没有双流分享权限"},
            {"SIG,421","对方终端不允许外人拨打，请与对方确认后再拨"},
            {"SIG,422","您已被主持人请出会议"},
            {"SIG,420","号码不正确"},
            {"CHARGE,481","对方不允许陌生人呼叫"},
            {"SIG,423","UNKONWN"},
            {"SIG,424","主持人已结束会议"},
            {"CHARGE,642","呼叫失败，企业IPPBX方数已达上限"},
            {"CHARGE,643","调度失败，企业监控调度方数已达上限"},
            {"CHARGE,640","所属部门的云会议室存储空间已满，不能再发起录制"},
            {"CHARGE,641","所属企业的云会议室存储空间已满，不能再发起录制"},
            {"CHARGE,635","账户余额不足，不能发起PSTN通话，请联系管理员充值后再尝试。"},
            {"CHARGE,636","云会议室已达允许呼入的上限"},
            {"CHARGE,633","无法呼叫当前号码，您的终端仅允许呼叫通讯录上的用户号码"},
            {"CHARGE,634","账户余额支付范围不可用于PSTN通话，请联系管理员开启后再尝试。"},
            {"CHARGE,639","云会议室存储空间已满，不能再发起录制"},
            {"CHARGE,637","云会议室已达部门允许呼入的上限"},
            {"CHARGE,638","云会议室已达企业允许呼入的上限"},
            {"SIG,419","对方已开启办公模式。您可滑动下方按钮，发起呼叫"},
            {"SIG,410","UNKONWN"},
            {"SIG,498","当前网络UDP被禁用，无法通话，请联系网络管理员或切换其他网络"},
            {"SIG,4445","云会议室存储空间已满，不能再发起录制"},
            {"SIG,411","终端APP版本过低，请升级后再试"},
            {"SIG,499","网络异常，呼叫已断开"},
            {"SIG,4446","所属部门的云会议室存储空间已满，不能再发起录制"},
            {"SIG,496","媒体超时"},
            {"SIG,4443","UNKONWN"},
            {"SIG,497","您的网络出现异常，请尝试重新进入"},
            {"SIG,4444","UNKONWN"},
            {"SIG,4449","无效的分享conten类型"},
            {"SIG,412","对方版本过低，通话无法接通"},
            {"SIG,4447","所属企业的云会议室存储空间已满，不能再发起录制"},
            {"SIG,413","对方设置了免打扰"},
            {"SIG,4448","未知错误"},
            {"SIG,490","无应答"},
            {"CHARGE,473","当前通话人数已满。如需更多人通话，请使用云会议室。"},
            {"SIG,491",""},
            {"CHARGE,474","当前会议人数已满。请加入通讯录，购买更多会议端口，详情请咨询本地经销商。"},
            {"CHARGE,471","当前通话人数已满。请使用终端或云会议室召开多方视频会议。"},
            {"CHARGE,472","呼叫无法接通，终端在家最多支持4方通话。"},
            {"SIG,4441","录制超时，请稍后再试"},
            {"CHARGE,477","您呼叫的会议已达到最大支持人数，请联系管理员购买或使用超大型会议室。"},
            {"CHARGE,631","此云会议室为按参会时长计费的会议室。您的账户余额已用尽，不能在此云会议室发起会议，请联系管理员充值或选择其他云会议室。"},
            {"SIG,4442","UNKONWN"},
            {"CHARGE,478","您的服务可用分钟数不足，无法呼叫。"},
            {"CHARGE,632","主动呼叫权限已被管理员关闭，无法发起呼叫"},
            {"CHARGE,475","当前会议人数已满，请联系管理员%s%s，及时为帐户充值，或购买更多会议端口。"},
            {"SIG,4440","分享content超时，请稍后再试"},
            {"CHARGE,476","您呼叫的会议已达到最大支持人数，请联系管理员购买或使用超大型会议室。"},
            {"CHARGE,630","当前云会议室不允许邀请通讯录外成员"},
            {"CHARGE,624","呼叫失败。您呼叫的终端端口使用服务已过期"},
            {"CHARGE,625","您呼叫的终端正在通话中，请稍后再呼。"},
            {"CHARGE,622","会议已达最大容量，无法加入更多参会人。"},
            {"REC,511","录制服务暂时不可用，请稍后再试"},
            {"CHARGE,623","呼叫失败。终端端口使用服务已过期，不能加入云会议室或多方通话。"},
            {"REC,512","录制服务暂时不可用，请稍后再试"},
            {"CHARGE,628","您呼叫的云会议室不允许匿名用户呼入，请登录或注册后重新呼叫"},
            {"CHARGE,629","对方正在会议中，且该会议不允许通讯录外成员呼入，请稍后再试"},
            {"CHARGE,626","当前限定为1对1通话，不能邀请更多人加入。"},
            {"CHARGE,627","呼叫的云会议室不允许通讯录外成员呼入，请联系云会议室管理员"},
            {"SIG,1503","分配监控资源失败"},
            {"REC,470","无可用录制服务器，请稍后再试"},
            {"SIG,4456","配置有误，请稍后重试"},
            {"SIG,4457","号码输入有误，请核对后重新拨打！"},
            {"SIG,4454","通话不存在，请核对后重试"},
            {"SIG,4455","服务器未连接，请稍后重试"},
            {"SIG,445","UNKONWN"},
            {"SIG,1501","shuttle获取MA失败"},
            {"SIG,4458","网络异常，呼叫已断开"},
            {"SIG,1502","shuttle获取媒体资源失败"},
            {"SIG,4459","号码输入有误，请核对后重新拨打！"},
            {"SIG,4452","录制失败"},
            {"CHARGE,620","无单位归属，不能呼叫H.323设备"},
            {"SIG,440","云会议室密码不正确，请重试"},
            {"SIG,4453","通话已存在，请稍后再试"},
            {"CHARGE,467","已达最大录制方数，不能发起录制"},
            {"CHARGE,621","呼叫失败，单位H.323方数不足"},
            {"SIG,4450","不支持分享content"},
            {"CHARGE,464","录制失败，尚未开通录制服务或服务已过期，请联系管理员购买。"},
            {"SIG,4451","带宽不足，分享content失败"},
            {"CHARGE,459","帐户余额不足，无法支持此呼叫。请联系管理员%s%s，及时为帐户充值。"},
            {"CHARGE,613","UNKONWN"},
            {"CHARGE,614","UNKONWN"},
            {"REC,400","信令服务与录制服务内部错误。"},
            {"CHARGE,457","终端电话可用分钟数不足，请充值；或联系管理员，加入通讯录。"},
            {"CHARGE,611","UNKONWN"},
            {"REC,401","录制服务暂时不可用，请稍后再试"},
            {"CHARGE,458","暂不支持呼叫国际电话。请联系管理员加入通讯录；或咨询本地经销商，为您的单位开通后台管理帐户。"},
            {"CHARGE,612","UNKONWN"},
            {"REC,402","录制服务暂时不可用，请稍后再试"},
            {"CHARGE,617","UNKONWN"},
            {"REC,403","录制服务暂时不可用，请稍后再试"},
            {"CHARGE,618","UNKONWN"},
            {"REC,404","录制不存在"},
            {"CHARGE,615","UNKONWN"},
            {"REC,405","录制服务暂时不可用，请稍后再试"},
            {"CHARGE,616","会议已被锁定，无法加入，请联系会议发起人解锁后进入。"},
            {"SIG,716","网络异常，呼叫已断开"},
            {"SIG,713","Content Server创建资源失败"},
            {"CHARGE,619","呼叫失败。终端的云服务端口已过期，请联系管理员续费购买。"},
            {"SIG,714","Failed to allocated resources for content sharing."},
            {"SIG,717","会议室已超出最高通话时长限制，转为语音通话模式，可联系管理员处理"},
            {"SIG,718","语音模式限制已解除"},
            {"SIG,432","UNKONWN"},
            {"SIG,433","UNKONWN"},
            {"SIG,430","Content发送时会议尚未开始"},
            {"SIG,431","Content Server创建资源失败INVALID_PASSWORD"},
            {"SIG,711","内容发送时会议尚未开始"},
            {"SIG,712","内容发送时会议尚未开始"},
            {"REC,406","录制服务暂时不可用，请稍后再试"},
            {"CHARGE,451","系统不支持此类型呼叫"},
            {"SIG,4460","号码输入有误，请核对后重新拨打！"},
            {"CHARGE,452","您的云会议室不支持邀请电话入会，请联系管理员加入通讯录；或咨询本地经销商，为您的开通帐户。"},
            {"CHARGE,450","您的终端目前无法支持此类呼叫"},{"CHARGE,455","暂不支持呼叫国际电话号码。"},
            {"CHARGE,610","呼叫失败，您的企业已禁止PSTN通话功能，请联系您的管理员！"},
            {"SIG,4461","号码输入有误，请核对后重新拨打！"},
            {"CHARGE,453","帐户余额不足，无法支持此呼叫。请联系管理员%s%s，及时为帐户充值。"},
            {"CHARGE,454","终端电话可用分钟数不足，无法呼叫"},
            {"CHARGE,602","当前通话人数已达上限，您无法加入。"},
            {"CHARGE,603","您呼叫的会议已达最大支持人数。设备加入通讯录，可支持更多方会议。如需更多支持请呼叫客服终端号’288288’咨询。"},
            {"CHARGE,601","会议容量使用现在达到上限，无法加入会议。请联系管理员%s%s购买更多会议端口。"},
            {"CHARGE,606","当前会议人数已超限，30分钟后将自动结束。请加入通讯录，可支持更多方会议。"},
            {"CHARGE,607","您的服务可用分钟数不足，无法呼叫。"},
            {"CHARGE,604","终端在家仅支持4方通话，当前通话人数已超限，30分钟后将自动结束。"},
            {"CHARGE,605","UNKONWN"},
            {"SIG,506","录制服务器内部错误"},
            {"SIG,1404","UNKONWN"},
            {"SIG,504","抱歉，呼叫失败，请再试一次"},
            {"CHARGE,608","您呼叫的会议已达最大支持人数，请联系IT管理员"},
            {"SIG,505","抱歉，呼叫失败，请再试一次"},
            {"CHARGE,609","您呼叫的会议已达最大支持人数，请联系IT管理员，购买超大型云会议室。"},
            {"SIG,469","录制正在进行，请勿重复发起录制"},
            {"SIG,502","抱歉，呼叫失败，请再试一次"},
            {"SIG,4999","分享content失败"},
            {"SIG,503","抱歉，呼叫失败，请再试一次"},
            {"CHARGE,441","当前在线呼叫数已达网关服务上限，请稍候重试"},
            {"SIG,461","录制时，本地空间即将耗尽"},
            {"SIG,462","录制时，本地空间已耗尽"},
            {"SIG,580","pstn呼叫时，第三方服务器报错"},
            {"REC,465","本地已开启录制，不能重复发起录制"},
            {"REC,466","录制服务暂时不可用，请稍后再试"},
            {"REC,467","已达最大录制方数，不能发起录制"},
            {"REC,468","录制的存储空间已满，您无法开始录制。请删除已有的录制视频，或联系管理员购买更多存储空间"},
            {"REC,469","UNKONWN"},
            {"REC,503","录制服务暂时不可用，请稍后再试"},
            {"REC,460","没有录制权限"},
            {"REC,463","录制的存储空间已满，您无法开始录制。请删除已有的录制视频，或联系管理员购买更多存储空间"},
            {"SIG,456","抱歉，呼叫失败，请再试一次"},
            {"REC,505","录制服务暂时不可用，请稍后再试"},
            {"REC,506","直播服务暂时不可用，请稍后再试"},
            {"REC,507","录制服务暂时不可用，请稍后再试"},
            {"REC,509","录制服务暂时不可用，请稍后再试"},
            {"SIG,408","对方没应答，请稍后再试"},
            {"SIG,405","对方设备不在线，请稍后重试"},
            {"SIG,409","建立多点呼叫时的内部消息"},
            {"SIG,487","呼叫已取消"},
            {"SIG,400","通讯失败，请升级到最新版本"},
            {"SIG,488","终端试用期已过，无法继续为您服务了"},
            {"SIG,485","已达最大呼叫时长，呼叫已断开"},
            {"SIG,486","对方忙，请稍后再试"},
            {"SIG,403","远端网络异常，呼叫已断开"},
            {"SIG,404","对方不在线"},
            {"SIG,401","加密算法不匹配"},
            {"SIG,480","抱歉，呼叫失败，请再试一次"},
            {"SIG,483","拨打不支持国家的pstn号码"},
            {"SIG,484","电话时长已用尽，无法转呼手机号，请联系IT管理员及时充值"},
            {"CHARGE,420","号码不正确"},
            {"SIG,482","邀请失败，请重试"},
            {"CHARGE,415","服务已过期，请管理员更新服务许可证"},
            {"CHARGE,416","当前在线呼叫数已达服务上限，请稍候再试"},
            {"CHARGE,414","H.323接入服务已过期"},
            {"STATE,200","成功"},
            {"SIG,598","抱歉，呼叫失败，请再试一次"},
            {"SIG,599","信令消息格式无效"},
            {"SIG,479","共享内容路数已达上限，无法发起更多的共享。请等待其他参会者停止共享后再重试"},
            {"CHARGE,650","h.323方数已达云会议室允许呼入的上限"},
            {"CHARGE,651","h.323方数已达部门允许呼入的上限"},
            {"CHARGE,652","预约会议已经过期，无法主动加入"},
            {"INVALID_PASSWORD","会议密码错误"},
            {"Invliad call number","请检查号码是否正确"},
            {"INVLIAD CALL NUMBER","请检查号码是否正确"}
        };
    }

    class FruitFactor
    {
        public static T CreateInstance<T>(string nameSpace, string className)
        {
            string fullClassName = nameSpace + "." + className;
            return (T)Assembly.Load(nameSpace).CreateInstance(fullClassName);
        }

        public static T CreateInstance<T>(string fullTypeName)
        {
            return (T)Activator.CreateInstance(Type.GetType(fullTypeName));
        }
    }

    public abstract class AbsFruit
    {
        protected string Name { get; set; }
        public abstract void Show();
    }


    class Strawberry : AbsFruit
    {
        public Strawberry()
        {
            Name = "草莓";
        }
        public override void Show()
        {
            Console.WriteLine("水果类型：" + Name);
        }
    }

    public class ReflectDemoClass
    {
        public void PrintText()
        {
            Console.WriteLine("我调用出来ReflectDemoClass PrintText方法了");
        }
    }
    /// <summary>
    /// Des 的摘要说明
    /// </summary>
    public class Des
    {
        public Des()
        {
        }

        public static string Encrypt(string stringToEncrypt, string sKey)
        {
            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.Mode = CipherMode.ECB;
            tdsp.Padding = PaddingMode.PKCS7;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(stringToEncrypt);
            des.Key = ASCIIEncoding.UTF8.GetBytes(sKey);
            des.IV = ASCIIEncoding.UTF8.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, tdsp.CreateEncryptor(des.Key, des.IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }


        /// <summary>

        /// DES加密字符串

        /// </summary>

        /// <param name="encryptString">待加密的字符串</param>

        /// <param name="encryptKey">加密密钥,要求为8位</param>

        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>

        public static object strEncryptDES(string encryptString, string encryptKey)

        {

            try

            {

                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));

                //rgbIV与rgbKey可以不一样，这里只是为了简便，读者可以自行修改

                byte[] rgbIV = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));

                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();

                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);

                cStream.FlushFinalBlock();

                return Convert.ToBase64String(mStream.ToArray());
                //return mStream.ToArray();

            }

            catch

            {

                return encryptString;

            }

        }

        public static string DesDecrypt(string pToDecrypt, string sKey)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return pToDecrypt;
            }
        }
    }
}
