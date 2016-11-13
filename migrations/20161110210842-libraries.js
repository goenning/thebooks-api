exports.up = function (db, done) {
  db.createTable('libraries', {
    id: { type: 'int', primaryKey: true, autoIncrement: true },
    name: { type: 'string', length: 40 },
    access_token: { type: 'string', length: 36 },
    created_on: 'datetime',
    modified_on: 'datetime'
  }, done);
  done();
};

exports.down = function (db, done) {
  db.dropTable('libraries', done);
};