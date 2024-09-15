using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTP_test
{
    public partial class Form1 : Form
    {
        Form2 f2;
        const int nCountOfImagesToGrab = 1;
        const string fAnaysis_ftp = "ftp://control-ideal.iptime.org:2126/Analysis";
        const string fLearning_ftp = "ftp://control-ideal.iptime.org:2126/Learning";
        public Form1()
        {
            InitializeComponent();
            f2 = new Form2();
        }

        private FtpWebResponse psFtpConnect(String url, string method, Action<FtpWebRequest> action = null)
        {
            var request = WebRequest.Create(url) as FtpWebRequest;
            request.UseBinary = true;
            request.Method = method;
            request.Credentials = new NetworkCredential("controlap", "!qazwsx!@34");
            if (action != null)
            {
                action(request);
            }
            return request.GetResponse() as FtpWebResponse;
        }


        private void btn_Connect_Click(object sender, EventArgs e)
        {
            string sUrl = textBox1.Text;
            using (var res = psFtpConnect(sUrl, WebRequestMethods.Ftp.ListDirectory))
            {
                using (var stream = res.GetResponseStream())
                {
                    using (var rd = new StreamReader(stream))
                    {
                        while (true)
                        {
                            string buf = rd.ReadLine();
                            if (string.IsNullOrWhiteSpace(buf))
                            {
                                break;
                            }
                            listBox1.Items.Add(buf);
                        }
                    }
                }
            }
        }
        private void UploadFileList(String url, string source) // ftp
        {
            var attr = File.GetAttributes(source);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo dir = new DirectoryInfo(source);
                foreach (var item in dir.GetFiles())
                {
                    try
                    {
                        UploadFileList(url + "/" + item.Name, item.FullName);
                        System.IO.File.Delete(Path.Combine(source, item.Name));//업로드 파일 삭제
                        //System.IO.File.Move(Path.Combine(source, item.Name), Path.Combine(imageDirectory + "_Backup", item.Name));
                    }
                    catch (WebException)
                    {
                        // 
                    }
                }

                foreach (var item in dir.GetDirectories())
                {
                    try
                    {
                        // ftp에 디렉토리를 생성한다.
                        psFtpConnect(url + "/" + item.Name, WebRequestMethods.Ftp.MakeDirectory).Close();
                        //System.IO.File.Delete(Path.Combine(source, item.Name));//업로드 파일 삭제
                        System.IO.File.Move(Path.Combine(source, item.Name), Path.Combine("C_Backup", item.Name));
                    }
                    catch (WebException)
                    {
                        // 
                    }
                    UploadFileList(url + "/" + item.Name, item.FullName);
                }
            }
            else
            {
                using (var fs = File.OpenRead(source))
                {

                    psFtpConnect(url, WebRequestMethods.Ftp.UploadFile, (req) =>
                    {
                        try
                        {
                            req.ContentLength = fs.Length;
                            using (var stream = req.GetRequestStream())
                            {
                                fs.CopyTo(stream);
                            }
                        }
                        catch (WebException)
                        {
                            // 
                        }
                    }).Close();

                }
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {

            string sUrl = "ftp://192.168.0.11//Data//HMI//정성운_백업";

            /*using (var fs = File.OpenRead("D:\\현재 프로젝트 모음\\c#\\Log_Del\\Log_Del\\bin\\Debug\\FTP_Send.txt"))
            {*/

            /*psFtpConnect(sUrl, WebRequestMethods.Ftp.UploadFile, (req) =>
            {
                try
                {
                   // req.ContentLength = fs.Length;
                    using (var stream = req.GetRequestStream())
                    {
                        StreamReader sSR = new StreamReader("D:\\현재 프로젝트 모음\\c#\\Log_Del\\Log_Del\\bin\\Debug\\FTP_Send.txt", Encoding.Default);
                        byte[] sSend_File = Encoding.UTF8.GetBytes(sSR.ReadToEnd());
                        sSR.Close();
                        stream.Write(sSend_File, 0, sSend_File.Length);
                        //fs.CopyTo(stream);
                    }
                }
                catch (WebException)
                {
                    // 
                }
            }).Close();*/
            // }

            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create("ftp://control-ideal.iptime.org/HDD3/Data/HMI/정성운_백업/에프티피.txt");
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            ftp.Credentials = new NetworkCredential("controlap", "!qazwsx!@34");

            StreamReader fileToSend = new StreamReader("D:\\현재 프로젝트 모음\\c#\\Log_Del\\Log_Del\\bin\\Debug\\에프티피.txt");
            byte[] fileContents = Encoding.UTF8.GetBytes(fileToSend.ReadToEnd());
            //fileToSend.Close(); //Close the open file

            Stream sendFile = ftp.GetRequestStream();
            sendFile.Write(fileContents, 0, fileContents.Length);
            sendFile.Close(); //Close the sending stream

            FtpWebResponse ftpResponse = (FtpWebResponse)ftp.GetResponse();
            listBox1.Items.Add(ftpResponse.StatusDescription); //Output the results of the transfer
            ftpResponse.Close();

            ftp = (FtpWebRequest)WebRequest.Create("ftp://control-ideal.iptime.org/HDD3/Data/HMI/");
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;
            ftp.Credentials = new NetworkCredential("controlap", "!qazwsx!@34");
            ftp.UseBinary = true;
            WebResponse sRes = ftp.GetResponse();
            sendFile = sRes.GetResponseStream();
            using (StreamReader use_SR = new StreamReader(sendFile, Encoding.Default))
            {
                while (true)
                {
                    string buf = use_SR.ReadLine();
                    if (string.IsNullOrWhiteSpace(buf))
                    {
                        break;
                    }
                    listBox1.Items.Add(buf);
                }
            }

            //ftp.Close(); //Close the FTP connection

        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            listView1.Items.Add("new world has come");
            ListViewItem lvi1 = new ListViewItem("dfdfd");

            lvi1.Text = "who are you";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f3 = new Form2();
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  psMain();
        }

//        private void psMain()
//        {
//            try
//            {
//                 Initialize StApi before using.
//                using (CStApiAutoInit api = new CStApiAutoInit())

//                 Create a system object for device scan and connection.
//                using (CStSystem system = new CStSystem())
                    
//                 Create a camera device object and connect to first detected device.
//                using (CStDevice device = system.CreateFirstStDevice())

//                 Create a DataStream object for handling image stream data.
//                using (CStDataStream dataStream = device.CreateStDataStream(0))
//                {
//                     Displays the DisplayName of the device.
//                    Debug.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

//                     Start the image acquisition of the host (local machine) side.
//                    dataStream.StartAcquisition(nCountOfImagesToGrab);

//                     Start the image acquisition of the camera side.
//                    device.AcquisitionStart();

//                     Get the path of the image files.
//                    string fileNameHeader = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
//                    fileNameHeader += @"\" + device.GetIStDeviceInfo().DisplayName + @"\" + device.GetIStDeviceInfo().DisplayName;
//                    Debug.WriteLine(fileNameHeader);
//                    bool isImageSaved = false;

//                     Get the file name of the image file of the StApiRaw file format
//                    string sTime = DateTime.Now.ToString("HHmmss");
//                    string fileNameRaw = fileNameHeader + $"{sTime}.StApiRaw";

//                     Retrieve the buffer of image data with a timeout of 5000ms.
//                    using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(1000))
//                    {
//                         Check if the acquired data contains image data.
//                        if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
//                        {
//                             If yes, we create a IStImage object for further image handling.
//                            IStImage stImage = streamBuffer.GetIStImage();
//                            Byte[] imageData = stImage.GetByteArray();

//                             Display the information of the acquired image data.
//                            Debug.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
//                            Debug.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
//                            Debug.Write(" First byte =" + imageData[0] + Environment.NewLine);

//                             Create a still image file handling class object (filer) for still image processing.
//                            using (CStStillImageFiler stillImageFiler = new CStStillImageFiler())
//                            {
//                                 Save the image file as StApiRaw file format with using the filer we created.
//                                Debug.Write(Environment.NewLine + "Saving " + fileNameRaw + "... ");
//                                stillImageFiler.Save(stImage, eStStillImageFileFormat.StApiRaw, fileNameRaw);
//                                Debug.Write("done" + Environment.NewLine);
//                                isImageSaved = true;
//                            }
//                        }
//                        else
//                        {
//                             If the acquired data contains no image data.
//                            Debug.WriteLine("Image data does not exist.");
//                        }
//                    }

//                     Stop the image acquisition of the camera side.
//                    device.AcquisitionStop();

//                     Stop the image acquisition of the host side.
//                    dataStream.StopAcquisition();

//                     The following code shows how to load the saved StApiRaw and process it.
//                    if (isImageSaved)
//                    {
//                         Create a buffer for storing the image data from StApiRaw file.
//                        using (CStImageBuffer imageBuffer = CStApiDotNet.CreateStImageBuffer())

//                         Create a still image file handling class object (filer) for still image processing.
//                        using (CStStillImageFiler stillImageFiler = new CStStillImageFiler())

//                         Create a data converter object for pixel format conversion.
//                        using (CStPixelFormatConverter pixelFormatConverter = new CStPixelFormatConverter())
//                        {
//                             Load the image from the StApiRaw file into buffer.
//                            Debug.Write(Environment.NewLine + "Loading " + fileNameRaw + "... ");
//                            stillImageFiler.Load(imageBuffer, fileNameRaw);
//                            Debug.Write("done" + Environment.NewLine);

//                             Convert the image data to BGR8 format.
//                            pixelFormatConverter.DestinationPixelFormat = eStPixelFormatNamingConvention.BGR8;
//                            pixelFormatConverter.Convert(imageBuffer.GetIStImage(), imageBuffer);

//                             Get the IStImage interface to the converted image data.
//                            IStImage stImage = imageBuffer.GetIStImage();

//                             Save as Bitmap
//                            {
//                                 Bitmap file extension.
//                                string imageFileName = fileNameHeader + $"{sTime}.bmp";

//                                 Save the image file in Bitmap format.
//                                Debug.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
//                                stillImageFiler.Save(stImage, eStStillImageFileFormat.Bitmap, imageFileName);
//                                Debug.Write("done" + Environment.NewLine);
//                            }

//                             Save as Tiff
//                            {
//                                 Tiff file extension.
//                                string imageFileName = fileNameHeader + $"{sTime}.tif";

//                                 Save the image file in Tiff format.
//                                Debug.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
//                                stillImageFiler.Save(stImage, eStStillImageFileFormat.TIFF, imageFileName);
//                                Debug.Write("done" + Environment.NewLine);
//                            }

//                             Save as PNG
//                            {
//                                 PNG file extension.
//                                string imageFileName = fileNameHeader + $"{sTime}.png";

//                                 Save the image file in PNG format.
//                                Debug.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
//                                stillImageFiler.Save(stImage, eStStillImageFileFormat.PNG, imageFileName);
//                                Debug.Write("done" + Environment.NewLine);
//                                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
//                                pictureBox1.BackgroundImage = Image.FromFile(imageFileName);
//                            }

//                             Save as JPEG
//                            {
//                                 JPEG file extension.
//                                string imageFileName = fileNameHeader + $"{sTime}.jpg";

//                                 Save the image file in JPEG format.
//                                stillImageFiler.Quality = 75;
//                                Debug.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
//                                stillImageFiler.Save(stImage, eStStillImageFileFormat.JPEG, imageFileName);
//                                Debug.Write("done" + Environment.NewLine);
//                            }

//                             Save as CSV
//                            {
//                                 CSV file extension.
//                                string imageFileName = fileNameHeader + $"{sTime}.csv";

//                                 Save the image file in CSV format.
//                                Debug.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
//                                stillImageFiler.Save(stImage, eStStillImageFileFormat.CSV, imageFileName);
//                                Debug.Write("done" + Environment.NewLine);
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                 If any exception occurred, display the description of the error here.
//                Debug.WriteLine("An exception occurred. \r\n" + e.Message);
//            }
//            finally
//            {
//            }
//        }
//        const string TRIGGER_SELECTOR = "TriggerSelector";              //Standard
//        const string TRIGGER_SELECTOR_FRAME_START = "FrameStart";       //Standard
//        const string TRIGGER_SELECTOR_EXPOSURE_START = "ExposureStart"; //Standard
//        const string TRIGGER_MODE = "TriggerMode";                      //Standard
//        const string TRIGGER_MODE_ON = "On";                            //Standard
//        const string TRIGGER_SOURCE = "TriggerSource";                  //Standard
//        const string TRIGGER_SOURCE_SOFTWARE = "Software";              //Standard
//        const string TRIGGER_SOFTWARE = "TriggerSoftware";				//Standard
//        void PsMain2()
//        {
//            try
//            {
//                 Initialize StApi before using.
//                using (CStApiAutoInit api = new CStApiAutoInit())

//                 Create a system object for device scan and connection.
//                using (CStSystem system = new CStSystem())

//                 Create a camera device object and connect to first detected device.
//                using (CStDevice device = system.CreateFirstStDevice())

//#if ENABLED_ST_GUI
//				 If using GUI for display, create a display window here.
//				using (CStImageDisplayWnd wnd = new CStImageDisplayWnd())
//#endif
//                 Create a DataStream object for handling image stream data.
//                using (CStDataStream dataStream = device.CreateStDataStream(0))
//                {
//                     Displays the DisplayName of the device.
//                    Debug.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

//                     Get the INodeMap interface for the camera settings.
//                    INodeMap nodeMapRemote = device.GetRemoteIStPort().GetINodeMap();

//                     Set the TriggerSelector to FrameStart.
//                    try
//                    {
//                        SetEnumeration(nodeMapRemote, TRIGGER_SELECTOR, TRIGGER_SELECTOR_FRAME_START);
//                    }
//                    catch (GenericException)
//                    {
//                         If "FrameStart" is not supported, use "ExposureStart".
//                        SetEnumeration(nodeMapRemote, TRIGGER_SELECTOR, TRIGGER_SELECTOR_EXPOSURE_START);
//                    }

//                     Set the TriggerMode to On.
//                    SetEnumeration(nodeMapRemote, TRIGGER_MODE, TRIGGER_MODE_ON);

//                     Set the TriggerSource to Software.
//                    SetEnumeration(nodeMapRemote, TRIGGER_SOURCE, TRIGGER_SOURCE_SOFTWARE);

//                     Get the ICommand interface for the TriggerSoftware node.
//                    ICommand commandNode = nodeMapRemote.GetNode<ICommand>(TRIGGER_SOFTWARE);

//                     Register a callback function. When a Data stream event is triggered, the registered function will be called.
//#if ENABLED_ST_GUI
//					object[] param = { wnd };
//					dataStream.RegisterCallbackMethod(OnCallback, param);
//#else
//                    dataStream.RegisterCallbackMethod(OnCallback, null);
//#endif
//                     Start the image acquisition of the host side.
//                    dataStream.StartAcquisition();

//                     Start the image acquisition of the camera side.
//                    device.AcquisitionStart();

//#if ENABLED_ST_GUI
//					 Create a node map display window object.
//					using (CStNodeMapDisplayWnd nodeMapDisplayWnd = new CStNodeMapDisplayWnd())
//					{
//						nodeMapDisplayWnd.RegisterINode(commandNode, "Root");
//						nodeMapDisplayWnd.Show(eStWindowMode.Modal);
//					}
//#else
//                    while (true)
//                    {
//                        Debug.WriteLine("0 : Generate trigger");
//                        Debug.WriteLine("Else : Exit");
//                        Debug.Write("Select : ");

//                         Waiting for input.
//                        string value = textBox1.Text;
//                        if (value != "")
//                        {
//                            int index;
//                            if (int.TryParse(value.Trim(), out index))
//                            {
//                                if (index == 0)
//                                {
//                                    commandNode.Execute();
//                                    System.Threading.Thread.Sleep(200);
//                                }
//                                else
//                                {
//                                    break;
//                                }
//                            }
//                            else
//                            {
//                                break;
//                            }
//                        }
//                    }
//#endif
//                     Stop the image acquisition of the camera side.
//                    device.AcquisitionStop();

//                     Stop the image acquisition of the host side.
//                    dataStream.StopAcquisition();
//                }
//            }
//            catch (Exception e)
//            {
//                 Display a description of the error.
//                Debug.WriteLine("An exception occurred. \r\n" + e.Message);
//            }
//        }
//        static void OnCallback(IStCallbackParamBase paramBase, object[] param)
//        {
//             Case of receiving a NewBuffer events.
//            if (paramBase.CallbackType == eStCallbackType.TL_DataStreamNewBuffer)
//            {
//                IStCallbackParamGenTLEventNewBuffer callbackParam = paramBase as IStCallbackParamGenTLEventNewBuffer;

//                if (callbackParam != null)
//                {
//                    try
//                    {
//                         Get the IStDataStream interface.
//                        IStDataStream dataStream = callbackParam.GetIStDataStream();

//                         Wait until the data is acquired.
//                         If the data has been received, CStStreamBuffer object is retrieved.
//                         Use the 'using' statement for automatically managing the buffer re-queue action when it's no longer needed.
//                        using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(0))
//                        {
//                            if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
//                            {
//                                 Get the IStImage interface to the acquired image data.
//                                IStImage stImage = streamBuffer.GetIStImage();
//#if ENABLED_ST_GUI
//								CStImageDisplayWnd wnd = param[0] as CStImageDisplayWnd;

//								 Check if display window is visible.
//								if (!wnd.IsVisible)
//								{
//									 Set the position and size of the window.
//									wnd.SetPosition(0, 0, (int)stImage.ImageWidth, (int)stImage.ImageHeight);

//									 Create a new thread to display the window.
//									wnd.Show(eStWindowMode.ModalessOnNewThread);
//								}

//								 Register the image to be displayed.
//								 This will have a copy of the image data and original buffer can be released if necessary.
//								wnd.RegisterIStImage(stImage);
//#else
//                                 Display the information of the acquired image data.
//                                Byte[] imageData = stImage.GetByteArray();
//                                Debug.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
//                                Debug.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
//                                Debug.Write(" First byte =" + imageData[0] + Environment.NewLine);
//#endif
//                            }
//                            else
//                            {
//                                 If the acquired data contains no image data.
//                                Debug.WriteLine("Image data does not exist");
//                            }
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                         Display a description of the error.
//                        Debug.WriteLine("An exception occurred. \r\n" + e.Message);
//                    }
//                }
//            }
//        }

//         Set Enumeration
//        static void SetEnumeration(INodeMap nodeMap, string enumerationName, string valueName)
//        {
//             Get the IEnum interface.
//            IEnum enumNode = nodeMap.GetNode<IEnum>(enumerationName);

//             Update the settings using the IEnum interface.
//            enumNode.StringValue = valueName;
//        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            try
            {

                FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create($"{fAnaysis_ftp}/IDEAL-020908S");
                requestFTPUploader.Credentials = new NetworkCredential("idealsys", "ideal2015!");
                requestFTPUploader.UseBinary = true;
                requestFTPUploader.UsePassive = true;
                requestFTPUploader.Timeout = 10000;
                requestFTPUploader.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse sResp = (FtpWebResponse)requestFTPUploader.GetResponse();
                Stream ftpStream = sResp.GetResponseStream();
                ftpStream.Close();
                sResp.Close();
            }
            catch (WebException exc)
            {
                listBox1.Items.Add(exc);
            }
        }
    }
}
