// Ignore Spelling: Reddit Utils

using Reddit.Integration.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit.Integration.Library.Utils {
    public static class MessageHelper {

        public static string GetMessage(MessageType messageType, string fieldName) {

            string message = string.Empty;

            switch (messageType) {
                case MessageType.ValdationError:
                    message= string.Format(MessageConstants.REDDIT_ERROR_INVALID_PARAMETER, fieldName);
                    break;
                case MessageType.UnknownError:
                    message = string.Format(MessageConstants.REDDIT_ERROR_UNKNOWN, fieldName);
                    break;
                default:
                    message = string.Format(MessageConstants.REDDIT_ERROR_INVALID_PARAMETER, fieldName);
                    break;
            }

            return message;

            
        }


    }
}
