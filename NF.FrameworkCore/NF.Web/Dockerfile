//#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
//#For more information, please see https://aka.ms/containercompat

//FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
//WORKDIR /app
//EXPOSE 80

//FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
//WORKDIR /src
//COPY ["NF.Web/NF.Web.csproj", "NF.Web/"]
//COPY ["NF.IBLL/NF.IBLL.csproj", "NF.IBLL/"]
//COPY ["NF.Common/NF.Common.csproj", "NF.Common/"]
//COPY ["NF.ViewModel/NF.ViewModel.csproj", "NF.ViewModel/"]
//COPY ["NF.Model/NF.Model.csproj", "NF.Model/"]
//COPY ["NF.BLL/NF.BLL.csproj", "NF.BLL/"]
//COPY ["NF.AutoMapper/NF.AutoMapper.csproj", "NF.AutoMapper/"]
//COPY ["NF.QuartzNet/NF.QuartzNet.csproj", "NF.QuartzNet/"]
//RUN dotnet restore "NF.Web/NF.Web.csproj"
//COPY . .
//WORKDIR "/src/NF.Web"
//RUN dotnet build "NF.Web.csproj" -c Release -o /app

//FROM build AS publish
//RUN dotnet publish "NF.Web.csproj" -c Release -o /app

//FROM base AS final
//WORKDIR /app
//COPY --from=publish /app .
//ENTRYPOINT ["dotnet", "NF.Web.dll"]
#添加基础镜像
FROM microsoft/dotnet:2.1-aspnetcore-runtime
#容器中系统的工作空间
WORKDIR /app
#拷贝当前文件夹下的文件到容器中系统的工作空间
COPY. /app

# 设置Docker容器对外暴露的端口
EXPOSE 5000
# 设置映射IP。不然默认只有8080可以
ENV ASPNETCORE_URLS http://+:5000
# 放了这个 ZKWeb.System.Drawing 验证码显示正常
RUN apt-get update
#容器中使用 ["dotnet","系统启动的dll"] 来运行应用程序
#使用ENTRYPOINT ["dotnet","系统启动的dll"]
#或使用 CMD ["dotnet","系统启动的dll"]
ENTRYPOINT ["dotnet", "NF.Web.dll"]