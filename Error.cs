﻿/*
 * Copyright (C)
 *   Arnaud Champion <arnaud.champion@devatom.fr>
 *   Jaromír Červenka <cervajz@cervajz.com>
 *
 * See COPYING.LIB for the License of this software
 */


using System;
using System.Runtime.InteropServices;

namespace Libvirt
{
    ///<summary>
    /// class for libvirt errors binding
    ///</summary>
    public class Error
    {
        /// <summary>
        /// The error object is kept in thread local storage, so separate threads can safely access this concurrently. 
        /// Reset the last error caught on that connection.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/> pointer to the hypervisor connection.
        /// </param>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnResetLastError")]
        public static extern void virConnResetLastError(IntPtr conn);

        /// <summary>
        /// Set a connection error handling function, if @handler is NULL it will reset to default 
        /// which is to pass error back to the global library handler.
        /// </summary>
        /// <param name="conn">
        /// A <see cref="IntPtr"/>pointer to the hypervisor connection.
        /// </param>
        /// <param name="userData">
        /// A <see cref="IntPtr"/>pointer to the user data provided in the handler callback.
        /// </param>
        /// <param name="handler">
        /// A <see cref="virErrorFunc"/>function to get called in case of error or NULL
        /// </param>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virConnSetErrorFunc")]
        public static extern void virConnSetErrorFunc(IntPtr conn, IntPtr userData, [MarshalAs(UnmanagedType.FunctionPtr)]virErrorFunc handler);

        /// <summary>
        /// Copy the content of the last error caught at the library level. 
        /// The error object is kept in thread local storage, so separate threads can safely access this concurrently. 
        /// One will need to free the result with virResetError().
        /// </summary>
        /// <param name="to">
        /// A <see cref="virError"/> target to receive the copy.
        /// </param>
        /// <returns>
        /// 0 if no error was found and the error code otherwise and -1 in case of parameter error.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virCopyLastError")]
        public static extern int virCopyLastError([Out] virError to);

        /// <summary>
        /// Default routine reporting an error to stderr.
        /// </summary>
        /// <param name="err">
        /// A <see cref="virError"/> pointer to the error.
        /// </param>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virDefaultErrorFunc")]
        public static extern void virDefaultErrorFunc([In] virError err);

        /// <summary>
        /// Resets and frees the given error.
        /// </summary>
        /// <param name="err">
        /// A <see cref="virError"/> error to free.
        /// </param>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virFreeError")]
        public static extern void virFreeError(virError err); // Does not work, anybody know why?

        /// <summary>
        /// Provide a pointer to the last error caught at the library level. 
        /// The error object is kept in thread local storage, so separate threads can safely access this concurrently.
        /// </summary>
        /// <returns>
        /// A pointer to the last error or NULL if none occurred.
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virGetLastError")]
        public static extern int virGetLastError();

        /// <summary>
        /// Reset the error being pointed to.
        /// </summary>
        /// <param name="err">
        /// A <see cref="virError"/> pointer to the to clean up.
        /// </param>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virResetError")]
        public static extern void virResetError(virError err);

        /// <summary>
        /// Reset the last error caught at the library level. The error object is kept in thread local storage, 
        /// so separate threads can safely access this concurrently, only resetting their own error object.
        /// </summary>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virResetLastError")]
        public static extern void virResetLastError();

        /// <summary>
        /// Save the last error into a new error object.
        /// </summary>
        /// <returns>
        /// A <see cref="virError"/> pointer to the copied error or NULL if allocation failed. 
        /// It is the caller's responsibility to free the error with virFreeError().
        /// </returns>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virSaveLastError")]
        public static extern virError virSaveLastError();

        /// <summary>
        /// Set a library global error handling function, if @handler is NULL, it will reset to default printing on stderr. 
        /// The error raised there are those for which no handler at the connection level could caught.
        /// </summary>
        /// <param name="userData">
        /// A <see cref="IntPtr"/>pointer to the user data provided in the handler callback.
        /// </param>
        /// <param name="handler">
        /// A <see cref="virErrorFunc"/>function to get called in case of error or NULL.
        /// </param>
        [DllImport("libvirt-0.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "virSetErrorFunc")]
        public static extern void virSetErrorFunc(IntPtr userData, [MarshalAs(UnmanagedType.FunctionPtr)]virErrorFunc handler);
    }
}