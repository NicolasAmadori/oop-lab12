namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double re;
        private readonly double im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real
        {
            get => re;
        }

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary
        {
            get => im;
        }

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus
        {
            get => Math.Sqrt(re * re + im * im);
        }

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase
        {
            get => Math.Atan2(re, im);
        }

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            if(re == 0 && im == 0) return "0";
            else if(re == 0) return ((im == 1) ? "i" : (im == -1 ? "-i" : im + "i"));
            else if (im == 0) return re.ToString();
            else return $"{re}{(im > 0 ? "+" : "")}{((im == 1) ? "i" : (im == -1 ? "-i" : im + "i"))}";
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other) => Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj) => obj is IComplex c && Equals(c);

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode() => HashCode.Combine(re, im);
    }
}
