﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Dtos;
using SharedLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
   public static class CustomExceptionHendler
    {
        //IApplicationBuilder classından türeyenler için yazılan ExceptionHandler classı
        public static void UseCustomException(this IApplicationBuilder app)
        {
            //Uygulama üzerindeki tüm hataları yakalan bir middleware
            app.UseExceptionHandler(config =>
            {
                //Run kullanılırsa Bu aşamadan sonra gelen istek, bir sonraki middleware a geçmez Çünkü bir hata meydana gelecek. Use ile kullanıldığında ise devam edeceği anlamına gelir
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json0";


                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;
                        ErrorDto errorDto = null;

                        if (ex is CustomException)
                        {
                            //Client tarafında bir hatadır ve kullanıcıya gösterilir
                            errorDto = new ErrorDto(ex.Message, true);
                        }
                        else
                        {
                            //Sistemin kendi hatasıdır ve kullanıcıya gösterilmez
                            errorDto = new ErrorDto(ex.Message, false);
                        }

                        var response = Response<NoDataDto>.Fail(errorDto, 500);
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                }); 
            });
        }
    }
}
