using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacksStand.WebAPI 
{
    public class EmailTemplates
    {
        #region Fields

        private const string _welcomeMessage = @"<div style='min-height: 300px;border: 1px solid black; max-height:450px; width: 80%; background-color: #FFFFFF; margin: auto; box-shadow: 10px 10px 5px #888888; '>
        <div style='background-color: #2DC3E8; padding: 4px 4px 4px 5px; text-align: center;'>
            <img src='{0}Assets/img/logo.png' alt='logo' height='50' width='50' />
        </div>
        <div style='padding: 10px 30px 10px 30px; font-size: 13px; font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif; '>
            <p>Dear {1} {2},</p>
            <p>
                Your account has been successfully created on <b>Racks Stand</b>.
                <a href='{3}'>Please click here</a> to  verify your email.
            </p>
            <p  >If you feel that you have received this message in error, or if you didn’t  verify your email, please contact us and let us know. </p>
            <p><b>Thank you!</b> <br />Team Racks Stand.</p>
        </div>
    </div>";

        private const string _accountVerifiedMessage = @"<div style='background-color: #f3f3f3; margin: 0px; padding: 0px; font-size: 13px; font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif;'>
        <div style='float:left; width:100%;'>
            <div style='float:left; width:100%; background:#2DC3E8; text-align:center; padding:15px 0;'>
                <div style='width:60%; margin:0 auto;'>
                    <img src='{0}Assets/img/logo.png' height='50' width='50' alt='logo' style='max-width:222px;'>
                </div>
            </div>
            <div style='float:left; width:100%; padding:30px 0;'>
                <div style='width:60%; min-height:300px; margin:0 auto;'>
                    <div style='float:left; width:100%; border:1px solid #dadbdd; border-radius:10px;background-color:#fff; font-weight:400; padding:0 0 10px;'>
                        <h2 style='font-weight:400; font-size:24px; margin:20px 0 0; padding:0 3%;line-height:34px;'>Hi, {0}</h2>
                        <p style='padding:0 3%'>Thank you so much for signing up for <b>Racks Stand</b>.</p>
                        <p style='padding:0 3%'>Your email has been successfully verifyed.</p>
                        <p style='padding:0 3%'>We’re excited to have you on board with us, and we can’t wait for you to get started.</p>
                        <p style='padding:0 3%'>Regards,</p>     <p style='padding:0 3%;'>Team Racks Stand</p>

                    </div>

                </div>
            </div>
            <div style='float:left; width:100%; background:#2DC3E8; text-align:center; padding:15px 0; margin:20px 0 0;'>
                <div style='width:60%; margin:0 auto;'>
                    <div style='float:right; width:100%;'>
                        <div style='float:left; width:100%; height:22px; text-align:center;'>
                            <a href='#twitter' style=' width: 30px; color: #fff; height: 22px; display: inline-block;'>
                                <img src='#' alt='tw' style='margin:0 4px; width:22px;'>
                            </a>
                            <a href='#facebook' style='width: 30px; color: #fff; height: 22px; display: inline-block;'>
                                <img src='#' alt='fb' style='margin:0 4px; width:22px;'>
                            </a>
                            <a href='#youtube' style='width: 30px; color: #fff; height: 22px; display: inline-block;'>
                                <img src='#' alt='youtube' style='margin:0 4px; width:22px;'>
                            </a>
                             
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>";
        private const string _forgetPasswordMessage = @"<div style='background-color: #f3f3f3; margin: 0px; padding: 0px; font-size: 13px; font-family: Helvetica Neue,Helvetica,Helvetica,Arial,sans-serif;'>
        <div style='float:left; width:100%;'>
            <div style='float: left; width: 100%; background:#2DC3E8; text-align: center; padding: 15px 0; height: 35px;'>
                <div style='width:60%; margin:0 auto;'>
                    <img src='{0}Assets/img/logo.png' height='50' width='50' alt='logo' style='height:35px;'>
                </div>
            </div>
            <div style='float:left; width:100%; padding:30px 0;'>
                <div style='width:60%; margin: 0 auto; '>
                    <div style='float:left; width:100%; border:1px solid #dadbdd; border-radius:10px;background-color:#fff; font-weight:400; padding:0 0 10px;'>
                        <h2 style='font-weight:400; font-size:24px; margin:20px 0 0; padding:0 3%;line-height:34px;'>Hi, {1} {2}</h2>
                        <p style='padding:0 3%;'>We have received your password reset request.</p>
                        <p style='padding:0 3%;'>If you requested this change, you can reset your password by following the link below:</p>
                        <p style='padding:0 3%;'>
                            <span style='color:#666; font-weight:600;'><a href='{3}'>Password Reset Link</a></span> <br>
                        </p>
                        <p style='padding:0 3%;'>Please note that this link is only valid for 24 hours. If you feel that you have received this message in error, or if you didn’t request a password reset, please contact us and let us know. </p>
                        <p style='padding:0 3%;'>Regards,</p>
                        <p style='padding:0 3%;'>Team Racks Stand</p>

                    </div>
                </div>
            </div>
            <div style='float:left; width:100%; background:#2DC3E8; text-align:center; padding:15px 0; margin:20px 0 0;'>
                <div style='width:60%; margin:0 auto;'>
                    <div style='float:right; width:100%;'>
                        <div style='float:left; width:100%; height:22px; text-align:center;'>
                            <a href='#twitter' style=' width: 30px; color: #fff; height: 22px; display: inline-block;'>
                                <img src='#' alt='tw' style='margin:0 4px; width:22px;'>
                            </a>
                            <a href='#facebook' style='width: 30px; color:#fff; height: 22px; display: inline-block; '>
                                <img src='#' alt='fb' style='margin:0 4px; width:22px;'>
                            </a>
                            <a href='#youtube' style='width: 30px; color: #fff; height: 22px; display: inline-block;'>
                                <img src='#' alt='youtube' style='margin:0 4px; width:22px;'>
                            </a>
                             
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>";

        #endregion
        
        #region Public
        /// <summary>
        /// Get welcome message email template. This email send when new user register/sinup with the application.
        /// </summary>
        /// <param name="firstName">User first name.</param>
        /// <param name="lastName">User last name.</param>
        /// <param name="url">User account verification link.</param>
        /// <returns>(string) template.</returns>
        public static string GetWelcomeMessage(string firstName, string lastName, string url)
        {
            return string.Format(_welcomeMessage, AppSettingManager.BaseURL, firstName, lastName, url);
        }
        /// <summary>
        /// Get user account verification email.This email sent when user verified his/her email.
        /// </summary>
        /// <returns>(string) template.</returns>
        public static string GetAccountVerifiedMessage()
        {
            return string.Format(_accountVerifiedMessage, AppSettingManager.BaseURL);
        }
        /// <summary>
        /// Get forgot password email template. This email send when user  request to reset/forgot password.
        /// </summary>
        /// <param name="firstName">User first name.</param>
        /// <param name="lastName">User last name.</param>
        /// <param name="url">User account verification link.</param>
        /// <returns>(string) template.</returns>
        public static string GetForgetPasswordMessage(string firstName, string lastName, string url)
        {
            return string.Format(_forgetPasswordMessage, AppSettingManager.BaseURL, firstName, lastName, url);
        }
        #endregion
    }
}