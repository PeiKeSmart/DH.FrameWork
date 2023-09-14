namespace DH.QRCode.Codec.Ecc;

public class ReedSolomon {
    internal int[] y;

    internal int[] gexp = new int[512];

    internal int[] glog = new int[256];

    internal int NPAR;

    internal int MAXDEG;

    internal int[] synBytes;

    internal int[] Lambda;

    internal int[] Omega;

    internal int[] ErrorLocs = new int[256];

    internal int NErrors;

    internal int[] ErasureLocs = new int[256];

    internal int NErasures = 0;

    internal bool correctionSucceeded = true;

    public virtual bool CorrectionSucceeded => correctionSucceeded;

    public virtual int NumCorrectedErrors => NErrors;

    public ReedSolomon(int[] source, int NPAR)
    {
        initializeGaloisTables();
        y = source;
        this.NPAR = NPAR;
        MAXDEG = NPAR * 2;
        synBytes = new int[MAXDEG];
        Lambda = new int[MAXDEG];
        Omega = new int[MAXDEG];
    }

    internal virtual void initializeGaloisTables()
    {
        int num6;
        int num5;
        int num4;
        int num3;
        int num2;
        int num;
        int num7 = (num6 = (num5 = (num4 = (num3 = (num2 = (num = 0))))));
        int num8 = 1;
        gexp[0] = 1;
        gexp[255] = gexp[0];
        glog[0] = 0;
        for (int i = 1; i < 256; i++)
        {
            int num9 = num;
            num = num2;
            num2 = num3;
            num3 = num4;
            num4 = num5 ^ num9;
            num5 = num6 ^ num9;
            num6 = num7 ^ num9;
            num7 = num8;
            num8 = num9;
            gexp[i] = num8 + num7 * 2 + num6 * 4 + num5 * 8 + num4 * 16 + num3 * 32 + num2 * 64 + num * 128;
            gexp[i + 255] = gexp[i];
        }
        for (int i = 1; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                if (gexp[j] == i)
                {
                    glog[i] = j;
                    break;
                }
            }
        }
    }

    internal virtual int gmult(int a, int b)
    {
        if (a == 0 || b == 0)
        {
            return 0;
        }
        int num = glog[a];
        int num2 = glog[b];
        return gexp[num + num2];
    }

    internal virtual int ginv(int elt)
    {
        return gexp[255 - glog[elt]];
    }

    internal virtual void decode_data(int[] data)
    {
        for (int i = 0; i < MAXDEG; i++)
        {
            int num = 0;
            for (int j = 0; j < data.Length; j++)
            {
                num = data[j] ^ gmult(gexp[i + 1], num);
            }
            synBytes[i] = num;
        }
    }

    public virtual void correct()
    {
        decode_data(y);
        correctionSucceeded = true;
        bool flag = false;
        for (int i = 0; i < synBytes.Length; i++)
        {
            if (synBytes[i] != 0)
            {
                flag = true;
            }
        }
        if (flag)
        {
            correctionSucceeded = correct_errors_erasures(y, y.Length, 0, new int[1]);
        }
    }

    internal virtual void Modified_Berlekamp_Massey()
    {
        int[] array = new int[MAXDEG];
        int[] array2 = new int[MAXDEG];
        int[] array3 = new int[MAXDEG];
        int[] array4 = new int[MAXDEG];
        init_gamma(array4);
        copy_poly(array3, array4);
        mul_z_poly(array3);
        copy_poly(array, array4);
        int num = -1;
        int num2 = NErasures;
        for (int i = NErasures; i < 8; i++)
        {
            int num3 = compute_discrepancy(array, synBytes, num2, i);
            if (num3 != 0)
            {
                for (int j = 0; j < MAXDEG; j++)
                {
                    array2[j] = array[j] ^ gmult(num3, array3[j]);
                }
                if (num2 < i - num)
                {
                    int num4 = i - num;
                    num = i - num2;
                    for (int j = 0; j < MAXDEG; j++)
                    {
                        array3[j] = gmult(array[j], ginv(num3));
                    }
                    num2 = num4;
                }
                for (int j = 0; j < MAXDEG; j++)
                {
                    array[j] = array2[j];
                }
            }
            mul_z_poly(array3);
        }
        for (int j = 0; j < MAXDEG; j++)
        {
            Lambda[j] = array[j];
        }
        compute_modified_omega();
    }

    internal virtual void compute_modified_omega()
    {
        int[] array = new int[MAXDEG * 2];
        mult_polys(array, Lambda, synBytes);
        zero_poly(Omega);
        for (int i = 0; i < NPAR; i++)
        {
            Omega[i] = array[i];
        }
    }

    internal virtual void mult_polys(int[] dst, int[] p1, int[] p2)
    {
        int[] array = new int[MAXDEG * 2];
        for (int i = 0; i < MAXDEG * 2; i++)
        {
            dst[i] = 0;
        }
        for (int i = 0; i < MAXDEG; i++)
        {
            for (int j = MAXDEG; j < MAXDEG * 2; j++)
            {
                array[j] = 0;
            }
            for (int j = 0; j < MAXDEG; j++)
            {
                array[j] = gmult(p2[j], p1[i]);
            }
            for (int j = MAXDEG * 2 - 1; j >= i; j--)
            {
                array[j] = array[j - i];
            }
            for (int j = 0; j < i; j++)
            {
                array[j] = 0;
            }
            for (int j = 0; j < MAXDEG * 2; j++)
            {
                dst[j] ^= array[j];
            }
        }
    }

    internal virtual void init_gamma(int[] gamma)
    {
        int[] array = new int[MAXDEG];
        zero_poly(gamma);
        zero_poly(array);
        gamma[0] = 1;
        for (int i = 0; i < NErasures; i++)
        {
            copy_poly(array, gamma);
            scale_poly(gexp[ErasureLocs[i]], array);
            mul_z_poly(array);
            add_polys(gamma, array);
        }
    }

    internal virtual void compute_next_omega(int d, int[] A, int[] dst, int[] src)
    {
        for (int i = 0; i < MAXDEG; i++)
        {
            dst[i] = src[i] ^ gmult(d, A[i]);
        }
    }

    internal virtual int compute_discrepancy(int[] lambda, int[] S, int L, int n)
    {
        int num = 0;
        for (int i = 0; i <= L; i++)
        {
            num ^= gmult(lambda[i], S[n - i]);
        }
        return num;
    }

    internal virtual void add_polys(int[] dst, int[] src)
    {
        for (int i = 0; i < MAXDEG; i++)
        {
            dst[i] ^= src[i];
        }
    }

    internal virtual void copy_poly(int[] dst, int[] src)
    {
        for (int i = 0; i < MAXDEG; i++)
        {
            dst[i] = src[i];
        }
    }

    internal virtual void scale_poly(int k, int[] poly)
    {
        for (int i = 0; i < MAXDEG; i++)
        {
            poly[i] = gmult(k, poly[i]);
        }
    }

    internal virtual void zero_poly(int[] poly)
    {
        for (int i = 0; i < MAXDEG; i++)
        {
            poly[i] = 0;
        }
    }

    internal virtual void mul_z_poly(int[] src)
    {
        for (int num = MAXDEG - 1; num > 0; num--)
        {
            src[num] = src[num - 1];
        }
        src[0] = 0;
    }

    internal virtual void Find_Roots()
    {
        NErrors = 0;
        for (int i = 1; i < 256; i++)
        {
            int num = 0;
            for (int j = 0; j < NPAR + 1; j++)
            {
                num ^= gmult(gexp[j * i % 255], Lambda[j]);
            }
            if (num == 0)
            {
                ErrorLocs[NErrors] = 255 - i;
                NErrors++;
            }
        }
    }

    internal virtual bool correct_errors_erasures(int[] codeword, int csize, int nerasures, int[] erasures)
    {
        NErasures = nerasures;
        for (int i = 0; i < NErasures; i++)
        {
            ErasureLocs[i] = erasures[i];
        }
        Modified_Berlekamp_Massey();
        Find_Roots();
        if (NErrors <= NPAR || NErrors > 0)
        {
            for (int j = 0; j < NErrors; j++)
            {
                if (ErrorLocs[j] >= csize)
                {
                    return false;
                }
            }
            for (int j = 0; j < NErrors; j++)
            {
                int i = ErrorLocs[j];
                int num = 0;
                for (int k = 0; k < MAXDEG; k++)
                {
                    num ^= gmult(Omega[k], gexp[(255 - i) * k % 255]);
                }
                int num2 = 0;
                for (int k = 1; k < MAXDEG; k += 2)
                {
                    num2 ^= gmult(Lambda[k], gexp[(255 - i) * (k - 1) % 255]);
                }
                int num3 = gmult(num, ginv(num2));
                codeword[csize - i - 1] ^= num3;
            }
            return true;
        }
        return false;
    }
}