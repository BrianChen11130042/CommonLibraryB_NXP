using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryB.Base.FiniteStateMachine
{
    public enum ES1
    {
        None,
        Init,
        Idle,
        Action,
        Error,
        Finish
    }

    public enum EHandshakeKey
    {
        None,
        Run,
        Finish,
    }

    public abstract partial class FSMBase<ES2, ES3>
    {
        public EHandshakeKey key { get; protected set; } = EHandshakeKey.None;
        public bool isError { get; protected set; }
        public int interval { get; set; } = 100;


        public async Task Run()
        {
            switch(S1)
            {
                case ES1.None:
                    break;

                case ES1.Init:
                    await Init();
                    break;

                case ES1.Idle:
                    await Idle();
                    break;

                case ES1.Action:
                    await Action();
                    break;

                case ES1.Error:
                    await Error();
                    break;

                case ES1.Finish:
                    await Finish();
                    break;
            }

            await Task.Delay(interval);
        }

        public abstract Task Init();
        public abstract Task Idle();
        public abstract Task Action();
        public abstract Task Error();
        public abstract Task Finish();
    }

    public abstract partial class FSMBase<ES2, ES3>
    {
        public ES1 S1 { get; set; } = ES1.None;
        public ES2 S2 { get;set; }
        public ES3 S3 { get; set; }

        private ES1 RS1 { get; set; }
        private ES2 RS2 { get; set; }
        private ES3 RS3 { get; set; }

        public void Set(ES1 s1, ES2 s2, ES3 s3)
        {
            S1 = s1;
            S2 = s2;
            S3 = s3;
        }

        public void Set(ES2 s2, ES3 s3)
        {
            S2 = s2;
            S3 = s3;
        }

        public void Set(ES3 s3)
        {
            S3 = s3;
        }

        public void SaveState()
        {
            RS1 = S1;
            RS2 = S2;
            RS3 = S3;
        }

        public void RetrieveState()
        {
            S1 = RS1;
            S2 = RS2;
            S3 = RS3;
        }
    }
}
