FROM node:14-alpine
ENV NODE_ENV=production
ENV NEXT_PUBLIC_API="http://173.22.246.146:9564/api"

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

EXPOSE 3000

CMD [ "npm", "start" ]