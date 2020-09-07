# Naruto.Subscribe

#### 介绍
发布订阅

#### 软件架构
1.    基于 net core 3.1
2. Redis使用的是自己根据StackExchanges.Redis封装的仓储 <https://gitee.com/haiboi/Naruto.Data>


#### 安装教程
1. 安装 <b>Naruto.Subscribe</b> 核心包
2. 安装 <b>Naruto.Subscribe.Provider.Redis</b> ，基于redis的发布订阅

#### 使用说明

1.  当前框架 默认使用的redis的发布订阅，更多mq的订阅正在接入中
2.  如果需要实现自己的订阅方式，需要实现接口 <b>INarutoPublish</b>发布接口，和<b>ISubscribeEvent</b>订阅接口
3. 书写自己的订阅业务的时候，需要继承<b>ISubscribe</b>接口,继承此接口的对象自动会注入成单例对象,可以使用DI的功能
4. ![subscribe](/subscribe.png)
5. 给需要订阅的方法标记<b>Subscribe</b>特性
6. 使用<b>NarutoMessageAopEvent</b>，可以处理消息发送前后的aop事件
#### 参与贡献 

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request