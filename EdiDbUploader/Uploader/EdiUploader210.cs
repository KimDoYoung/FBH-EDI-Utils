using EdiDbUploader;
using FBH.EDI.Common.Model;

namespace EdiDbUploader
{
    internal class EdiUploader210 : EdiUploader
    {
        public override void Insert(EdiDocument ediDoc)
        {
            base.Insert(ediDoc);
            
        }

    }
}